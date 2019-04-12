using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECG_DB;

namespace ECG.Models
{
    public class EcgModels
    {
        public class Pediatrico : IElectrocardiograma
        {
            
            #region P
            public int ritmo_id { get; set; }

            public string deriv_di_ondap { get; set; }

            public string deriv_avf_ondap { get; set; }

            public bool ondaP_antes_qrs { get; set; }

            public bool seq_pqrs_todos_complejo { get; set; }

            public int intervalo_pr { get; set; }

            public int amplitud_ondap { get; set; }

            public int duracion_ondap { get; set; }

            #endregion

            #region QRS
            public string deriv_di_qrs_r_u_s { get; set; }
            public string deriv_avf_qrs_r_u_s { get; set; }
            public bool complejos_qrs { get; set; }
            public int duracion_qrs { get; set; }
            public int amplitud_r_v1_v2 { get; set; }
            public int amplitud_r_v5_v6 { get; set; }
            public int amplitud_s_v1_v2 { get; set; }
            public int amplitud_s_v5_v6 { get; set; }
            public string qrs_isodifico_en { get; set; }

            public string predominio_s_r_en { get; set; }
            public string predominio_s_r_val { get; set; }
            #endregion

            #region QTST

            public int intervalo_qt { get; set; }
            public int r_r { get; set; }
            public bool st_isoelectrico { get; set; }
            public string onda_t_v1_v2 { get; set; }
            public string onda_t_v5_v6 { get; set; }

            public int morfologiaqrs_id { get; set; }
            #endregion

            #region Q




            public bool presencia_onda_q { get; set; }

            public string pico_mas_alto_en { get; set; }

            public int profundida_ondaq { get; set; }

            public int duracion_ondaq { get; set; }


            #endregion

        }

        public class Adulto : IElectrocardiograma
        {
            #region P
            public int ritmo_id { get; set; }
            public string deriv_di_ondap { get; set; }
            public string deriv_avf_ondap { get; set; }
            public bool ondaP_antes_qrs { get; set; }
            public bool seq_pqrs_todos_complejo { get; set; }
            public int intervalo_pr { get; set; }
            public int amplitud_ondap { get; set; }
            public int duracion_ondap { get; set; }
            public bool observa_aleteo_auricular { get; set; }
            #endregion

            #region QRS
            public string deriv_di_qrs_r_u_s { get; set; }
            public string deriv_avf_qrs_r_u_s { get; set; }
            public bool complejos_qrs { get; set; }
            public int duracion_qrs { get; set; }
            public int amplitud_r_v1_v2 { get; set; }
            public int amplitud_r_v5_v6 { get; set; }
            public int amplitud_s_v1_v2 { get; set; }
            public int amplitud_s_v5_v6 { get; set; }
            public string qrs_isodifico_en { get; set; }
            public string predominio_s_r_en{ get; set; }
            public string predominio_s_r_val { get; set; }

            #endregion

            #region QTST
            
            public int intervalo_qt { get; set; }
            public int r_r { get; set; }
            public bool st_isoelectrico { get; set; }
            public string onda_t_v1_v2 { get; set; }
            public string onda_t_v5_v6 { get; set; }
            public int morfologiaqrs_id { get; set; }

            #endregion

            #region ST  

            public bool infradesnivel_st { get; set; }
            public string infradesnivel_st_en { get; set; }
            public bool supradesnivel_st { get; set; }
            public string supradesnivel_st_en { get; set; }
#endregion

            #region Q
 
            public bool presencia_onda_q { get; set; }
            public string pico_mas_alto_en { get; set; }
            public int profundida_ondaq { get; set; }
            public int duracion_ondaq { get; set; }

            #endregion
        }

        public class EstudiosRealizados
        { 
            public decimal EcgID { get; set; }

            public DateTime Fecha_Estudio { get; set; }

            public decimal? Paciente_Id { get; set; }

            public string Paciente_Nombre { get; set; }

            public string paciente_tipo { get; set; }

            public string Motivo_Consulta { get; set; }

            public string Tecnico { get; set; }
        }
    }
}