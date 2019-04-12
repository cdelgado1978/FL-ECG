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
    public class HomeController : Controller
    {

        EcgEntities db = new EcgEntities();

        #region Adulto

        

        [HttpGet]
        public ActionResult AdultWizard(int id = 0)
        {
            ViewBag.Message = "Adulto";

            ViewBag.ecgId = id;
            EcgModels.Adulto adulto = new EcgModels.Adulto();

            if (id != 0)
            {
                decimal ecgId = Convert.ToDecimal(id);
                
                var estudio = db.ecgs.Where(e => e.ecg_id == ecgId && e.pediatrico == false);

                if (estudio.Any())
                {
                    ViewBag.NombrePx = estudio.FirstOrDefault()?.paciente.FullName;

                    var detalle = estudio.FirstOrDefault()?.ecg_detalle;

                    CargaECGDetalleAdulto(detalle, adulto);
                }


            }

            LlenaListas();

            return View(adulto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdultWizard(int ecg_id, EcgModels.Adulto adulto)
        {

            ViewBag.Message = "Adulto";

            if (ModelState.IsValid)
            {
                var existEcgDetalle = AgregaOActualizaEcgDetalleAdulto(ecg_id, adulto);

                return RedirectToAction("VerReporte", "Reportes", routeValues: new { ecgId = ecg_id });
            }

            return View(adulto);
        }

        #endregion

     
 
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InfoPediatric()
        {
            return View();
        }

        public ActionResult InfoAdult()
        {
            return View();
        }


        public ActionResult NuevoECG()
        {
            NuevoECGModel nuevoEcg = new NuevoECGModel();

            var MotivosLst = db.motivos_visita.Where(mv => mv.activo == true).ToList();
            var pacientesLst = db.pacientes.Where(p => p.activo == true).ToList();

            var motivoSelectList = new SelectList(MotivosLst, "motivo_id", "motivo");
            var pacientesSelectList = new SelectList(pacientesLst, "paciente_id", "FullName");
            ViewBag.Motivos = motivoSelectList;
            ViewBag.Pacientes = pacientesSelectList;

            return View(nuevoEcg);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NuevoECG([Bind(Include = "paciente_id, fecha_nacimiento,fecha_estudio ,motivo_id,usuarioResponsable")] NuevoECGModel captura)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var adulto = ((DateTime.Now.Year - captura.fecha_nacimiento.Year) >= 15) ? true : false;

                    ecg ecg = new ecg()
                    {
                        digitado_por = captura.usuarioResponsable,
                        fecha_estudio = DateTime.Now,
                        paciente_id = captura.paciente_id,
                        pediatrico = !adulto,
                        motivo_id = captura.motivo_id
                    };

                    db.ecgs.Add(ecg);

                    db.SaveChanges();



                    if (adulto)
                    {
                        return RedirectToAction("AdultWizard", routeValues: new { id = ecg.ecg_id });
                    }

                    return RedirectToAction("PediatricWizard", routeValues: new {id= ecg.ecg_id});
                }

                var MotivosLst = db.motivos_visita.Where(mv => mv.activo == true).ToList();
                var pacientesLst = db.pacientes.Where(p => p.activo == true).ToList();

                var motivoSelectList = new SelectList(MotivosLst, "motivo_id", "motivo");
                var pacientesSelectList = new SelectList(pacientesLst, "paciente_id", "FullName");
                ViewBag.Motivos = motivoSelectList;
                ViewBag.Pacientes = pacientesSelectList;

                return View(captura);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        #region pediatrico

        //public ActionResult Pediatrico(int id = 0)
        //{
        //    ViewBag.Message = "Pediatrico.";

        //    ViewBag.ecgId = id;

        //    EcgModels.Pediatrico pediatrico = new EcgModels.Pediatrico();

        //    if (id != 0)
        //    {
        //        decimal ecgId = Convert.ToDecimal(id);

        //        var detalle = db.ecg_detalle.Where(e => e.ecg_id == ecgId);
        //        var estudio = db.ecgs.Where(e => e.ecg_id == ecgId);

        //        if (estudio.Any())
        //        {
        //            ViewBag.NombrePx = estudio.FirstOrDefault().paciente.FullName;

        //        }

        //        CargaECGDetallePediatrico(detalle, pediatrico);
        //    }

        //    LlenaListas();

        //    return View(pediatrico);
        //}

        public ActionResult PediatricWizard(int id = 0)
        {

            ViewBag.Message = "Pediatrico.";

            ViewBag.ecgId = id;

            EcgModels.Pediatrico pediatrico = new EcgModels.Pediatrico();

            if (id != 0)
            {

                decimal ecgId = Convert.ToDecimal(id);
                
                var estudio = db.ecgs.Where(e => e.ecg_id == ecgId && e.pediatrico == true);

                if (estudio.Any())
                {
                    ViewBag.NombrePx = estudio.FirstOrDefault()?.paciente.FullName;

                    var detalle = db.ecg_detalle.Where(e => e.ecg_id == ecgId);

                    CargaECGDetallePediatrico(detalle, pediatrico);
                }

                
            }

            LlenaListas();

            return View(pediatrico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PediatricWizard(int ecg_id, EcgModels.Pediatrico pediatrico)
        {

            if (ModelState.IsValid)
            {
                var existEcgDetalle = AgregaOActualizaEcgDetallePediatrico(ecg_id, pediatrico);


                return RedirectToAction("VerReporte", "Reportes", routeValues: new { ecgId = ecg_id });
            }


            return RedirectToAction("Index");
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Pediatrico(int ecg_id, EcgModels.Pediatrico pediatrico)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        var existEcgDetalle = AgregaOActualizaEcgDetallePediatrico(ecg_id, pediatrico);

        //        return ImprimirReporte(ecg_id);
        //    }


        //    return RedirectToAction("Index");
        //}


        #endregion




        #region Carga Datos

        private static void CargaECGDetalleAdulto(IEnumerable<ecg_detalle> detalle, EcgModels.Adulto adulto)
        {
            if (detalle.Any())
            {
                var _detalle = detalle.First();

                /*  --- P --- */
                adulto.ritmo_id = int.Parse(_detalle.ritmo_id.ToString());
                adulto.deriv_di_ondap = _detalle.deriv_di_ondap;
                adulto.deriv_avf_ondap = _detalle.deriv_avf_ondap;
                adulto.ondaP_antes_qrs = Convert.ToBoolean(_detalle.ondap_antes_qrs);
                adulto.seq_pqrs_todos_complejo = Convert.ToBoolean(_detalle.seq_pqrs_todos_complejo);
                adulto.duracion_ondap = Convert.ToInt32(_detalle.duracion_ondap);
                adulto.amplitud_ondap = Convert.ToInt32(_detalle.amplitud_ondap);
                adulto.intervalo_pr = Convert.ToInt32(_detalle.intervalo_pr);
                /* --- P --- */

                /*--- QRS ---*/
                adulto.deriv_di_qrs_r_u_s = _detalle.deriv_di_qrs_r_u_s;
                adulto.deriv_avf_qrs_r_u_s = _detalle.deriv_avf_qrs_r_u_s;
                adulto.complejos_qrs = Convert.ToBoolean(_detalle.complejos_qrs);
                adulto.morfologiaqrs_id = Convert.ToInt32(_detalle.morfologiaqrs_id);
                adulto.duracion_qrs = Convert.ToInt32(_detalle.duracion_qrs);
                adulto.amplitud_r_v1_v2 = Convert.ToInt32(_detalle.amplitud_r_v1_v2);
                adulto.amplitud_s_v1_v2 = Convert.ToInt32(_detalle.amplitud_s_v1_v2);
                adulto.amplitud_r_v5_v6 = Convert.ToInt32(_detalle.amplitud_r_v5_v6);
                adulto.amplitud_s_v5_v6 = Convert.ToInt32(_detalle.amplitud_s_v5_v6);
                adulto.qrs_isodifico_en = _detalle.qrs_isodifasico_en;
                //pediatrico.predo = (int)_detalle.amplitud_r_v5_v6;
                /*--- QRS ---*/

                /*--- QTST ---*/
                adulto.intervalo_qt = int.Parse(_detalle.intervalo_qt.ToString());
                adulto.r_r = int.Parse(_detalle.r_r.ToString());
                adulto.st_isoelectrico = Convert.ToBoolean(_detalle.st_isoelectrico);
                adulto.onda_t_v1_v2 = _detalle.onda_t_v1_v2;
                adulto.onda_t_v5_v6 = _detalle.onda_t_v5_v6;
                /*--- QTST ---*/

                /*--- Q ---*/
                adulto.presencia_onda_q = Convert.ToBoolean(_detalle.presencia_onda_q);
                adulto.pico_mas_alto_en = _detalle.pico_mas_alto_en;
                adulto.profundida_ondaq = int.Parse(_detalle.profundida_ondaq.ToString());
                adulto.duracion_ondaq = int.Parse(_detalle.duracion_ondaq.ToString());
                /*--- Q ---*/

                adulto.supradesnivel_st = Convert.ToBoolean(_detalle.supradesnivel_st);
                adulto.supradesnivel_st_en = _detalle.supradesnivel_st_en;
                adulto.infradesnivel_st = Convert.ToBoolean(_detalle.infradesnivel_st);
                adulto.infradesnivel_st_en = _detalle.infradesnivel_st_en;

            }
        }


        private static void CargaECGDetallePediatrico(IQueryable<ecg_detalle> detalle, EcgModels.Pediatrico pediatrico)
        {
            if (detalle.Any())
            {
                var _detalle = detalle.First();

                /*  --- P --- */
                pediatrico.ritmo_id = int.Parse(_detalle.ritmo_id.ToString());
                pediatrico.deriv_di_ondap = _detalle.deriv_di_ondap;
                pediatrico.deriv_avf_ondap = _detalle.deriv_avf_ondap;
                pediatrico.ondaP_antes_qrs = Convert.ToBoolean(_detalle.ondap_antes_qrs);
                pediatrico.seq_pqrs_todos_complejo = Convert.ToBoolean(_detalle.seq_pqrs_todos_complejo);
                pediatrico.duracion_ondap = Convert.ToInt32(_detalle.duracion_ondap);
                pediatrico.amplitud_ondap = Convert.ToInt32(_detalle.amplitud_ondap);
                pediatrico.intervalo_pr = Convert.ToInt32(_detalle.intervalo_pr);
                /* --- P --- */

                /*--- QRS ---*/
                pediatrico.deriv_di_qrs_r_u_s = _detalle.deriv_di_qrs_r_u_s;
                pediatrico.deriv_avf_qrs_r_u_s = _detalle.deriv_avf_qrs_r_u_s;
                pediatrico.complejos_qrs = Convert.ToBoolean(_detalle.complejos_qrs);
                pediatrico.morfologiaqrs_id = Convert.ToInt32(_detalle.morfologiaqrs_id);
                pediatrico.duracion_qrs = Convert.ToInt32(_detalle.duracion_qrs);
                pediatrico.amplitud_r_v1_v2 = Convert.ToInt32(_detalle.amplitud_r_v1_v2);
                pediatrico.amplitud_s_v1_v2 = Convert.ToInt32(_detalle.amplitud_s_v1_v2);
                pediatrico.amplitud_r_v5_v6 = Convert.ToInt32(_detalle.amplitud_r_v5_v6);
                pediatrico.amplitud_s_v5_v6 = Convert.ToInt32(_detalle.amplitud_s_v5_v6);
                pediatrico.qrs_isodifico_en = _detalle.qrs_isodifasico_en;
                //pediatrico.predo = (int)_detalle.amplitud_r_v5_v6;
                /*--- QRS ---*/

                /*--- QTST ---*/
                pediatrico.intervalo_qt = int.Parse(_detalle.intervalo_qt.ToString());
                pediatrico.r_r = int.Parse(_detalle.r_r.ToString());
                pediatrico.st_isoelectrico = Convert.ToBoolean(_detalle.st_isoelectrico);
                pediatrico.onda_t_v1_v2 = _detalle.onda_t_v1_v2;
                pediatrico.onda_t_v5_v6 = _detalle.onda_t_v5_v6;
                /*--- QTST ---*/

                /*--- Q ---*/
                pediatrico.presencia_onda_q = Convert.ToBoolean(_detalle.presencia_onda_q);
                pediatrico.pico_mas_alto_en = _detalle.pico_mas_alto_en;
                pediatrico.profundida_ondaq = int.Parse(_detalle.profundida_ondaq.ToString());
                pediatrico.duracion_ondaq = int.Parse(_detalle.duracion_ondaq.ToString());
                /*--- Q ---*/
            }
        }


        public string getFechaNacimientoPaciente(int id)
        {
            var _paciente = db.pacientes.FirstOrDefault(p => p.paciente_id == id);

            var FechaPX =  _paciente?.fecha_nacimiento.Date.ToString("dd/MM/yyyy") ?? DateTime.Today.Date.ToString("dd/MM/yyyy");

            return FechaPX;

        }
        #endregion


        
        private ecg_detalle AgregaOActualizaEcgDetalleAdulto(int ecg_id, EcgModels.Adulto adulto)
        {
            var existEcgDetalle = db.ecg_detalle.FirstOrDefault(e => e.ecg_id == ecg_id);

            if (existEcgDetalle == null)
            {
                ecg_detalle detalle = new ecg_detalle();

                detalle.ecg_id = ecg_id;
                /*  --- P --- */
                detalle.ritmo_id = adulto.ritmo_id;
                detalle.deriv_di_ondap = adulto.deriv_di_ondap;
                detalle.deriv_avf_ondap = adulto.deriv_avf_ondap;
                detalle.ondap_antes_qrs = adulto.ondaP_antes_qrs;
                detalle.seq_pqrs_todos_complejo = adulto.seq_pqrs_todos_complejo;
                detalle.duracion_ondap = adulto.duracion_ondap;
                detalle.amplitud_ondap = adulto.amplitud_ondap;
                detalle.intervalo_pr = adulto.intervalo_pr;
                detalle.observa_aleteo_auricular = adulto.observa_aleteo_auricular;
                /* --- P --- */

                /*--- QRS ---*/
                detalle.deriv_di_qrs_r_u_s = adulto.deriv_di_qrs_r_u_s;
                detalle.deriv_avf_qrs_r_u_s = adulto.deriv_avf_qrs_r_u_s;
                detalle.complejos_qrs = adulto.complejos_qrs;
                detalle.morfologiaqrs_id = adulto.morfologiaqrs_id;
                detalle.duracion_qrs = adulto.duracion_qrs;
                detalle.amplitud_r_v1_v2 = adulto.amplitud_r_v1_v2;
                detalle.amplitud_s_v1_v2 = adulto.amplitud_s_v1_v2;
                detalle.amplitud_r_v5_v6 = adulto.amplitud_r_v5_v6;
                detalle.amplitud_s_v5_v6 = adulto.amplitud_s_v5_v6;
                detalle.qrs_isodifasico_en = adulto.qrs_isodifico_en;
                detalle.predominio_s_r_en = adulto.predominio_s_r_en;
                /*--- QRS ---*/

                /*--- QTST ---*/
                detalle.intervalo_qt = adulto.intervalo_qt;
                detalle.r_r = adulto.r_r;
                detalle.st_isoelectrico = adulto.st_isoelectrico;
                detalle.onda_t_v1_v2 = adulto.onda_t_v1_v2;
                detalle.onda_t_v5_v6 = adulto.onda_t_v5_v6;
                /*--- QTST ---*/

                /*--- Q ---*/
                detalle.presencia_onda_q = adulto.presencia_onda_q;
                detalle.pico_mas_alto_en = adulto.pico_mas_alto_en;
                detalle.profundida_ondaq = adulto.profundida_ondaq;
                detalle.duracion_ondaq = adulto.duracion_ondaq;
                /*--- Q ---*/

                /*--- ST ---*/
                detalle.supradesnivel_st = adulto.supradesnivel_st;
                detalle.supradesnivel_st_en = adulto.supradesnivel_st_en;
                detalle.infradesnivel_st = adulto.infradesnivel_st;
                detalle.infradesnivel_st_en = adulto.infradesnivel_st_en;
                /*--- ST ---*/

                db.ecg_detalle.Add(detalle);
            }
            else
            {
                //detalle.ecg_id = ecg_id;

                /*  --- P --- */
                existEcgDetalle.ritmo_id = adulto.ritmo_id;
                existEcgDetalle.deriv_di_ondap = adulto.deriv_di_ondap;
                existEcgDetalle.deriv_avf_ondap = adulto.deriv_avf_ondap;
                existEcgDetalle.ondap_antes_qrs = adulto.ondaP_antes_qrs;
                existEcgDetalle.seq_pqrs_todos_complejo = adulto.seq_pqrs_todos_complejo;
                existEcgDetalle.duracion_ondap = adulto.duracion_ondap;
                existEcgDetalle.amplitud_ondap = adulto.amplitud_ondap;
                existEcgDetalle.intervalo_pr = adulto.intervalo_pr;
                /* --- P --- */

                /*--- QRS ---*/
                existEcgDetalle.deriv_di_qrs_r_u_s = adulto.deriv_di_qrs_r_u_s;
                existEcgDetalle.deriv_avf_qrs_r_u_s = adulto.deriv_avf_qrs_r_u_s;
                existEcgDetalle.complejos_qrs = adulto.complejos_qrs;
                existEcgDetalle.morfologiaqrs_id = adulto.morfologiaqrs_id;
                existEcgDetalle.duracion_qrs = adulto.duracion_qrs;
                existEcgDetalle.amplitud_r_v1_v2 = adulto.amplitud_r_v1_v2;
                existEcgDetalle.amplitud_s_v1_v2 = adulto.amplitud_s_v1_v2;
                existEcgDetalle.amplitud_r_v5_v6 = adulto.amplitud_r_v5_v6;
                existEcgDetalle.amplitud_s_v5_v6 = adulto.amplitud_s_v5_v6;
                existEcgDetalle.qrs_isodifasico_en = adulto.qrs_isodifico_en;
                existEcgDetalle.predominio_s_r_en = adulto.predominio_s_r_en;
                
                /*--- QRS ---*/

                /*--- QTST ---*/
                existEcgDetalle.intervalo_qt = adulto.intervalo_qt;
                existEcgDetalle.r_r = adulto.r_r;
                existEcgDetalle.st_isoelectrico = adulto.st_isoelectrico;
                existEcgDetalle.onda_t_v1_v2 = adulto.onda_t_v1_v2;
                existEcgDetalle.onda_t_v5_v6 = adulto.onda_t_v5_v6;
                /*--- QTST ---*/

                /*--- Q ---*/
                existEcgDetalle.presencia_onda_q = adulto.presencia_onda_q;
                existEcgDetalle.pico_mas_alto_en = adulto.pico_mas_alto_en;
                existEcgDetalle.profundida_ondaq = adulto.profundida_ondaq;
                existEcgDetalle.duracion_ondaq = adulto.duracion_ondaq;
                /*--- Q ---*/
                /*--- ST ---*/
                existEcgDetalle.infradesnivel_st = adulto.infradesnivel_st;
                existEcgDetalle.infradesnivel_st_en = adulto.infradesnivel_st_en;
                existEcgDetalle.supradesnivel_st = adulto.supradesnivel_st;
                existEcgDetalle.supradesnivel_st_en = adulto.supradesnivel_st_en;
                /*--- ST ---*/
            }

            db.SaveChanges();
            return existEcgDetalle;
        }

        private ecg_detalle AgregaOActualizaEcgDetallePediatrico(int ecg_id, EcgModels.Pediatrico pediatrico)
        {
            var existEcgDetalle = db.ecg_detalle.FirstOrDefault(e => e.ecg_id == ecg_id);

            if (existEcgDetalle == null)
            {
                ecg_detalle detalle = new ecg_detalle();

                detalle.ecg_id = ecg_id;
                /*  --- P --- */
                detalle.ritmo_id = pediatrico.ritmo_id;
                detalle.deriv_di_ondap = pediatrico.deriv_di_ondap;
                detalle.deriv_avf_ondap = pediatrico.deriv_avf_ondap;
                detalle.ondap_antes_qrs = pediatrico.ondaP_antes_qrs;
                detalle.seq_pqrs_todos_complejo = pediatrico.seq_pqrs_todos_complejo;
                detalle.duracion_ondap = pediatrico.duracion_ondap;
                detalle.amplitud_ondap = pediatrico.amplitud_ondap;
                detalle.intervalo_pr = pediatrico.intervalo_pr;
                /* --- P --- */

                /*--- QRS ---*/
                detalle.deriv_di_qrs_r_u_s = pediatrico.deriv_di_qrs_r_u_s;
                detalle.deriv_avf_qrs_r_u_s = pediatrico.deriv_avf_qrs_r_u_s;
                detalle.complejos_qrs = pediatrico.complejos_qrs;
                detalle.morfologiaqrs_id = pediatrico.morfologiaqrs_id;
                detalle.duracion_qrs = pediatrico.duracion_qrs;
                detalle.amplitud_r_v1_v2 = pediatrico.amplitud_r_v1_v2;
                detalle.amplitud_s_v1_v2 = pediatrico.amplitud_s_v1_v2;
                detalle.amplitud_r_v5_v6 = pediatrico.amplitud_r_v5_v6;
                detalle.amplitud_s_v5_v6 = pediatrico.amplitud_s_v5_v6;
                detalle.qrs_isodifasico_en = pediatrico.qrs_isodifico_en;
                detalle.predominio_s_r_en = pediatrico.predominio_s_r_en;
                /*--- QRS ---*/

                /*--- QTST ---*/
                detalle.intervalo_qt = pediatrico.intervalo_qt;
                detalle.r_r = pediatrico.r_r;
                detalle.st_isoelectrico = pediatrico.st_isoelectrico;
                detalle.onda_t_v1_v2 = pediatrico.onda_t_v1_v2;
                detalle.onda_t_v5_v6 = pediatrico.onda_t_v5_v6;
                /*--- QTST ---*/

                /*--- Q ---*/
                detalle.presencia_onda_q = pediatrico.presencia_onda_q;
                detalle.pico_mas_alto_en = pediatrico.pico_mas_alto_en;
                detalle.profundida_ondaq = pediatrico.profundida_ondaq;
                detalle.duracion_ondaq = pediatrico.duracion_ondaq;
                /*--- Q ---*/

                db.ecg_detalle.Add(detalle);
            }
            else
            {
                //detalle.ecg_id = ecg_id;

                /*  --- P --- */
                existEcgDetalle.ritmo_id = pediatrico.ritmo_id;
                existEcgDetalle.deriv_di_ondap = pediatrico.deriv_di_ondap;
                existEcgDetalle.deriv_avf_ondap = pediatrico.deriv_avf_ondap;
                existEcgDetalle.ondap_antes_qrs = pediatrico.ondaP_antes_qrs;
                existEcgDetalle.seq_pqrs_todos_complejo = pediatrico.seq_pqrs_todos_complejo;
                existEcgDetalle.duracion_ondap = pediatrico.duracion_ondap;
                existEcgDetalle.amplitud_ondap = pediatrico.amplitud_ondap;
                existEcgDetalle.intervalo_pr = pediatrico.intervalo_pr;
                /* --- P --- */

                /*--- QRS ---*/
                existEcgDetalle.deriv_di_qrs_r_u_s = pediatrico.deriv_di_qrs_r_u_s;
                existEcgDetalle.deriv_avf_qrs_r_u_s = pediatrico.deriv_avf_qrs_r_u_s;
                existEcgDetalle.complejos_qrs = pediatrico.complejos_qrs;
                existEcgDetalle.morfologiaqrs_id = pediatrico.morfologiaqrs_id;
                existEcgDetalle.duracion_qrs = pediatrico.duracion_qrs;
                existEcgDetalle.amplitud_r_v1_v2 = pediatrico.amplitud_r_v1_v2;
                existEcgDetalle.amplitud_s_v1_v2 = pediatrico.amplitud_s_v1_v2;
                existEcgDetalle.amplitud_r_v5_v6 = pediatrico.amplitud_r_v5_v6;
                existEcgDetalle.amplitud_s_v5_v6 = pediatrico.amplitud_s_v5_v6;
                existEcgDetalle.qrs_isodifasico_en = pediatrico.qrs_isodifico_en;
                existEcgDetalle.predominio_s_r_en = pediatrico.predominio_s_r_en;

                //pediatrico.predo = (int)_detalle.amplitud_r_v5_v6;
                /*--- QRS ---*/

                /*--- QTST ---*/
                existEcgDetalle.intervalo_qt = pediatrico.intervalo_qt;
                existEcgDetalle.r_r = pediatrico.r_r;
                existEcgDetalle.st_isoelectrico = pediatrico.st_isoelectrico;
                existEcgDetalle.onda_t_v1_v2 = pediatrico.onda_t_v1_v2;
                existEcgDetalle.onda_t_v5_v6 = pediatrico.onda_t_v5_v6;
                /*--- QTST ---*/

                /*--- Q ---*/
                existEcgDetalle.presencia_onda_q = pediatrico.presencia_onda_q;
                existEcgDetalle.pico_mas_alto_en = pediatrico.pico_mas_alto_en;
                existEcgDetalle.profundida_ondaq = pediatrico.profundida_ondaq;
                existEcgDetalle.duracion_ondaq = pediatrico.duracion_ondaq;
                /*--- Q ---*/
            }

            db.SaveChanges();
            return existEcgDetalle;
        }


        private void LlenaListas()
        {
            /* -------  DATA  ------- */
            var ritmoLst = db.ecg_ritmo.ToList();
            var morfologiaList = db.morfologia_qrs.ToList();


            /* -------  SELECT LIST  ------- */
            var ritmosSelectList = new SelectList(ritmoLst, "ritmo_id", "ritmo");
            
            var morfologiaSelectList = new SelectList(morfologiaList, "morfologiaqrs_id", "morfologia_qrs1");
            /* -------  VIEW BAGS  ------- */
            ViewBag.ritmos = ritmosSelectList;
            ViewBag.morfologia = morfologiaSelectList;

        }
    }
}