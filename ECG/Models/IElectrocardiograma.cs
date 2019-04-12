using ECG_DB;

namespace ECG.Models
{
    public interface IElectrocardiograma
    {

        int ritmo_id { get; set; }

        int amplitud_ondap { get; set; }
        int amplitud_r_v1_v2 { get; set; }
        int amplitud_r_v5_v6 { get; set; }
        int amplitud_s_v1_v2 { get; set; }
        int amplitud_s_v5_v6 { get; set; }
        bool complejos_qrs { get; set; }
        string deriv_avf_ondap { get; set; }
        string deriv_avf_qrs_r_u_s { get; set; }
        string deriv_di_ondap { get; set; }
        string deriv_di_qrs_r_u_s { get; set; }
        int duracion_ondap { get; set; }
        int duracion_ondaq { get; set; }
        int duracion_qrs { get; set; }
       
        int intervalo_pr { get; set; }
        int intervalo_qt { get; set; }
        int morfologiaqrs_id { get; set; }
        string onda_t_v1_v2 { get; set; }
        string onda_t_v5_v6 { get; set; }
        bool ondaP_antes_qrs { get; set; }
        string pico_mas_alto_en { get; set; }
        string predominio_s_r_en { get; set; }
        string predominio_s_r_val { get; set; }

        bool presencia_onda_q { get; set; }
        int profundida_ondaq { get; set; }
        string qrs_isodifico_en { get; set; }
        int r_r { get; set; }
        bool seq_pqrs_todos_complejo { get; set; }
        bool st_isoelectrico { get; set; }
  
    }
}