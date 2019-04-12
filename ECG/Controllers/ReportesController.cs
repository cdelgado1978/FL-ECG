using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using ECG.Models;
using ECG_DB;


namespace ECG.Controllers
{
    public class ReportesController : Controller
    {

        EcgEntities db = new EcgEntities();

  
        #region Reportes


        public ActionResult VerReporte(int ecgId)
        {

            var ecgEstudio = db.ecgs.FirstOrDefault(e => e.ecg_id == ecgId);
            bool pediatrico = false;

            if (ecgEstudio != null)
            {
                if (!ecgEstudio.ecg_detalle.Any()) return HttpNotFound("Detalle no encontrado.");

                var ecgdt = ecgEstudio.ecg_detalle.First();

                pediatrico = Convert.ToBoolean(ecgEstudio.pediatrico);

                var datosInforme = CargaDatosInforme(ecgId, ecgEstudio, ecgdt).FirstOrDefault();

                if (pediatrico) return View("informePediatrico", datosInforme);
              

                return View("informeAdulto", datosInforme);
            }

            return HttpNotFound();
        }

        public ActionResult ImprimirReporte(int ecgId)
        {
            ReportDocument rd = new ReportDocument();

            var ecgEstudio = db.ecgs.FirstOrDefault(e => e.ecg_id == ecgId);

            if (ecgEstudio != null)
            {
                if (!ecgEstudio.ecg_detalle.Any()) return HttpNotFound("Detalle no encontrado.");

                var ecgdt = ecgEstudio.ecg_detalle.First();

                bool pediatrico = Convert.ToBoolean(ecgEstudio.pediatrico);

                string pxFullName = ecgEstudio.paciente.FullName;
                string fechaEstudio = ecgEstudio.fecha_estudio.Date.ToString("yyyy/MM/dd");

                var datosInforme = CargaDatosInforme(ecgId, ecgEstudio, ecgdt);

                var nombreArchivoReporte = NombreArchivoReporte(ecgId, pediatrico, pxFullName, ecgEstudio, fechaEstudio);


                rd.Load(pediatrico
                    ? Path.Combine(Server.MapPath("~/Reporte"), "ecg_pediatria.rpt")
                    : Path.Combine(Server.MapPath("~/Reporte"), "ecg_adultos.rpt"));

                rd.SetDataSource(datosInforme);
                rd.Refresh();

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

                stream.Seek(0, SeekOrigin.Begin);


                return File(stream, "application/pdf", nombreArchivoReporte);
            }

            return HttpNotFound();

        }

        public ActionResult ReporteXDia()
        {
            IEnumerable<EcgModels.EstudiosRealizados> _estudiosList = new List<EcgModels.EstudiosRealizados>();
            return View(_estudiosList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReporteXDia([Bind(Include = "fechaInicial,fechaFinal")] DateTime fechaInicial, DateTime fechaFinal)
        {

     
            var result = from ec in db.ecgs
                         where ec.fecha_estudio >= fechaInicial.Date && ec.fecha_estudio <= fechaFinal.Date
                         select new EcgModels.EstudiosRealizados
                         {
                             EcgID = ec.ecg_id,
                             Fecha_Estudio = ec.fecha_estudio,
                             Motivo_Consulta = ec.motivos_visita.motivo,
                             Paciente_Id = ec.paciente_id,
                             Paciente_Nombre = ec.paciente.nombre + " " + ec.paciente.apellidos,
                             paciente_tipo = (ec.pediatrico.Value) ? "Pediatrico" : "Adulto",
                             Tecnico = ec.digitado_por
                         };

           

            var count = result.Count();

            return View(result);
        }

        private static string NombreArchivoReporte(int ecgId, bool pediatrico, string pxFullName, ecg ecgEstudio,
            string fechaEstudio)
        {
            string nombreArchivoReporte = (pediatrico) ? $"ecg_pediatrico_{ecgId}" : $"ecg_adulto_{ecgId}";


            if (pxFullName != null) nombreArchivoReporte += $"_{ecgEstudio.paciente.FullName.Replace(" ", "").Trim()}";

            nombreArchivoReporte += $"_{fechaEstudio}.pdf";
            return nombreArchivoReporte;
        }

        #endregion

        private static IEnumerable<InformeEcgModel> CargaDatosInforme(int ecgId, ecg ecgEstudio, ecg_detalle ecgdt)
        {
            try
            {

                InformeEcgModel DatosPediatricosInforme = new InformeECGPediatricoModel();
                InformeEcgModel DatosAdultosInforme = new InformeECGAdultosModel();

                return ((bool)ecgEstudio.pediatrico)
                    ? MapReportData(ecgId, ecgEstudio, ecgdt, DatosPediatricosInforme)
                    : MapReportData(ecgId, ecgEstudio, ecgdt, DatosAdultosInforme);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static IEnumerable<InformeEcgModel> MapReportData(int ecgId, ecg ecgEstudio, ecg_detalle ecgdt, InformeEcgModel DatosInforme)
        {
            DatosInforme.ecg_id = ecgId;
            DatosInforme.fecha_estudio = ecgEstudio.fecha_estudio;
            DatosInforme.nombre = ecgEstudio.paciente.nombre;
            DatosInforme.apellidos = ecgEstudio.paciente.apellidos;
            DatosInforme.fecha_nacimiento = ecgEstudio.paciente.fecha_nacimiento;
            DatosInforme.motivo_id = Convert.ToInt32(ecgEstudio.motivo_id);
            DatosInforme.motivo = (ecgEstudio.motivo_id != null) ? ecgEstudio.motivos_visita.motivo : string.Empty;
            DatosInforme.pediatrico = Convert.ToBoolean(ecgEstudio.pediatrico);
            DatosInforme.ritmo_id = Convert.ToInt32(ecgdt.ritmo_id);
            DatosInforme.ritmo = ecgdt.ecg_ritmo.ritmo;
            DatosInforme.deriv_di_ondap = ecgdt.deriv_di_ondap;
            DatosInforme.deriv_avf_ondap = ecgdt.deriv_avf_ondap;
            DatosInforme.ondap_antes_qrs = Convert.ToBoolean(ecgdt.ondap_antes_qrs);
            DatosInforme.seq_pqrs_todos_complejo = Convert.ToBoolean(ecgdt.seq_pqrs_todos_complejo);
            DatosInforme.intervalo_pr = Convert.ToInt32(ecgdt.intervalo_pr);
            DatosInforme.amplitud_ondap = Convert.ToInt32(ecgdt.amplitud_ondap);
            DatosInforme.duracion_ondap = Convert.ToInt32(ecgdt.duracion_ondap);
            DatosInforme.deriv_di_qrs_r_u_s = ecgdt.deriv_di_qrs_r_u_s;
            DatosInforme.deriv_avf_qrs_r_u_s = ecgdt.deriv_avf_qrs_r_u_s;
            DatosInforme.complejos_qrs = Convert.ToBoolean(ecgdt.complejos_qrs);
            DatosInforme.duracion_qrs = Convert.ToInt32(ecgdt.duracion_qrs);
            DatosInforme.amplitud_r_v1_v2 = Convert.ToInt32(ecgdt.amplitud_r_v1_v2);
            DatosInforme.amplitud_r_v5_v6 = Convert.ToInt32(ecgdt.amplitud_r_v5_v6);
            DatosInforme.amplitud_s_v1_v2 = Convert.ToInt32(ecgdt.amplitud_s_v1_v2);
            DatosInforme.amplitud_s_v5_v6 = Convert.ToInt32(ecgdt.amplitud_s_v5_v6);
            DatosInforme.qrs_isodifasico_en = ecgdt.qrs_isodifasico_en;
            DatosInforme.predominio_s_r_en = ecgdt.predominio_s_r_en;
            //DatosInforme.predominio_s_r_val = ecgdt.predominio_s_r_val;
            DatosInforme.intervalo_qt = Convert.ToInt32(ecgdt.intervalo_qt);
            DatosInforme.r_r = Convert.ToInt32(ecgdt.r_r);
            DatosInforme._stIsoelectrico = Convert.ToBoolean(ecgdt.st_isoelectrico);
            DatosInforme.onda_t_v1_v2 = ecgdt.onda_t_v1_v2;
            DatosInforme.onda_t_v5_v6 = ecgdt.onda_t_v5_v6;
            DatosInforme.presencia_onda_q = Convert.ToBoolean(ecgdt.presencia_onda_q);
            DatosInforme.pico_mas_alto_en = ecgdt.pico_mas_alto_en;
            DatosInforme.profundida_ondaq = Convert.ToInt32(ecgdt.profundida_ondaq);
            DatosInforme.duracion_ondaq = Convert.ToInt32(ecgdt.duracion_ondaq);
            if (ecgdt.supradesnivel_st != null)
                DatosInforme.supradesnivel_st =
                    Convert.ToBoolean((bool) (ecgdt.supradesnivel_st));
            DatosInforme.supradesnivel_st_en = ecgdt.supradesnivel_st_en;
            if (ecgdt.infradesnivel_st != null)
                DatosInforme.infradesnivel_st = Convert.ToBoolean((bool) (ecgdt.infradesnivel_st));
            DatosInforme.infradesnivel_st_en = ecgdt.infradesnivel_st_en;
            DatosInforme.morfologia_qrs = ecgdt.morfologia_qrs.morfologia_qrs1;
            DatosInforme.digitado_por = ecgEstudio.digitado_por;

            return new[] { DatosInforme };
        }
    }
}