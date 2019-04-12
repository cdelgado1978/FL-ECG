using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ECG.Models
{

    public class InformeECGPediatricoModel : InformeEcgModel
    {

        public override string eje_comentario
        {
            get
            {

                switch (deriv_di_qrs_r_u_s)
                {

                    case "S" when deriv_avf_qrs_r_u_s == "S":
                        return "Patológico";
                    case "R" when deriv_avf_qrs_r_u_s == "S":
                        return "Patológico";
                    case "S" when deriv_avf_qrs_r_u_s == "R":
                        return "Evaluar Resultado";
                    default:
                        return string.Empty;
                }
            }
        }


        public override string seq_pqrs_todos_complejo_comentarios => (seq_pqrs_todos_complejo)
            ? "Secuencia P-QRS en todos los complejos"
            : "No se verifica secuencia P-QRS en todos los complejos";

        public override string FrecuenciaCardiaca_dx
        {
            get
            {
                if (ritmo.ToLower() == "irregular") return string.Empty;

                if (edad_dias >= 0 && edad_dias <= 28)
                {
                    if (FrecuenciaCardiaca_ComplejoQRS <= 95) return "Bradicardia";
                    if (FrecuenciaCardiaca_ComplejoQRS >= 150) return "Taquicardia";
                }
                else if (edad_dias >= 28 && edad_dias <= 89)
                {
                    if (FrecuenciaCardiaca_ComplejoQRS <= 121) return "Bradicardia";
                    if (FrecuenciaCardiaca_ComplejoQRS >= 179) return "Taquicardia";
                }
                else if (edad_dias >= 90 && edad_dias <= 179)
                {
                    if (FrecuenciaCardiaca_ComplejoQRS <= 106) return "Bradicardia";
                    if (FrecuenciaCardiaca_ComplejoQRS >= 186) return "Taquicardia";
                }
                else if (edad_dias >= 180 && edad_dias <= 359)
                {
                    if (FrecuenciaCardiaca_ComplejoQRS <= 109) return "Bradicardia";
                    if (FrecuenciaCardiaca_ComplejoQRS >= 169) return "Taquicardia";
                }
                else if (edad_dias >= 360 && edad_dias <= 1079)
                {
                    if (FrecuenciaCardiaca_ComplejoQRS <= 89) return "Bradicardia";
                    if (FrecuenciaCardiaca_ComplejoQRS >= 151) return "Taquicardia";
                }
                else if (edad_dias >= 1080 && edad_dias <= 1799)
                {
                    if (FrecuenciaCardiaca_ComplejoQRS <= 73) return "Bradicardia";
                    if (FrecuenciaCardiaca_ComplejoQRS >= 137) return "Taquicardia";
                }
                else if (edad_dias >= 1800 && edad_dias <= 2879)
                {
                    if (FrecuenciaCardiaca_ComplejoQRS <= 65) return "Bradicardia";
                    if (FrecuenciaCardiaca_ComplejoQRS >= 133) return "Taquicardia";
                }
                else if (edad_dias >= 2880 && edad_dias <= 4319)
                {
                    if (FrecuenciaCardiaca_ComplejoQRS <= 62) return "Bradicardia";
                    if (FrecuenciaCardiaca_ComplejoQRS >= 130) return "Taquicardia";
                }
                else if (edad_dias >= 4320)
                {
                    if (FrecuenciaCardiaca_ComplejoQRS <= 60) return "Bradicardia";
                    if (FrecuenciaCardiaca_ComplejoQRS >= 119) return "Taquicardia";
                }

                return "Normal para la edad";
            }


        }

        public override string seg_pqrs_evalucion {
            get
            {
             
                    if (edad_anos > 3)
                    {
                        return (seq_pqrs_evaluacion_grado == 1) ? "Patológico" : "DLN";
                    }

                    return "Evaluar";
                }
            
        }

        public override string amplitud_r_v1_v2_dx => (amplitud_r_v1_v2 > 12.1) 
            ? "Aumentado(supera el p98 para edad)" 
            : "DLN (no supera p98 para edad)";

        public override string amplitud_r_v1_v2_dx2
        {
            get
            {
                if (amplitud_r_v1_v2_dx != "Aumentado(supera el p98 para edad)") return string.Empty;
                return (eje != "+90º y +-180º") ? string.Empty : "Hipertrofia VD?";
            }
        }
        public override string amplitud_s_v1_v2_dx => (amplitud_s_v1_v2 > 25.4) 
            ? "Aumentado(supera el p98 para edad)" 
            : "DLN (no supera p98 para edad)";


        public override string amplitud_s_v1_v2_dx2
        {
            get
            {
                if (amplitud_s_v1_v2_dx != "Aumentado(supera el p98 para edad)") return string.Empty;
                return (eje != "0º y -90º") ? string.Empty : "Hipertrofia VI?";
            }
        }
        public override string amplitud_r_v5_v6_dx => (amplitud_r_v5_v6 > 25.4) 
            ? "Aumentado (supera el p98 para edad)" 
            : "DLN (no supera p98 para edad)";

        public override string amplitud_r_v5_v6_dx2
        {
            get
            {
                if (amplitud_r_v5_v6_dx != "Aumentado(supera el p98 para edad)") return string.Empty;
                return (eje != "0º y -90º") ? string.Empty : "Hipertrofia VI?";
            }
            //=> (eje_comentario != string.Empty) ? "Hipertrofia VI?" : "--";
        }

        public override string amplitud_s_v5_v6_dx => (amplitud_s_v5_v6 > 3.9) 
            ? "Aumentado (supera el p98 para edad)" 
            : "DLN (no supera p98 para edad)";

        public override string amplitud_s_v5_v6_dx2
        {
            get
            {
                if (amplitud_s_v5_v6_dx != "Aumentado(supera el p98 para edad)") return string.Empty;
                return (eje != "+90º y +-180º") ? string.Empty : "Hipertrofia VD?";
            }
            //=> (eje_comentario != string.Empty) ? "Hipertrofia VD?" : "--";
        }


        #region SupraDesnivel

        public override string supradesnivel_st_en
        {
            get => string.Empty;

            set => _supradesnivel_st_en = value;
        }

        public override string supradesnivel_st_Observacion => string.Empty;
        public override string supradesnivel_st_recomendacion => string.Empty;

        #endregion
        #region InfraDesnivel

        public override string infradesnivel_st_text => string.Empty;

        public override string infradesnivel_st_en
        {
            get => string.Empty;
            set => _infradesnivel_st_en = value;
        }

        public override string infradesnivel_st_Observacion => string.Empty;
        public override string infradesnivel_st_recomendacion => string.Empty;

        #endregion

    }

    public class InformeECGAdultosModel : InformeEcgModel
    {

        public override string eje_comentario
        {
            get
            {

                switch (deriv_di_qrs_r_u_s)
                {

                    case "S" when deriv_avf_qrs_r_u_s == "S":
                        return "Patológico";
                    case "R" when deriv_avf_qrs_r_u_s == "S":
                        return "Patológico";
                    case "S" when deriv_avf_qrs_r_u_s == "R":
                        return "Patológico (si más de 110°)";
                    default:
                        return "Normal";
                }
            }
        }
        public override string seg_pqrs_evalucion
        {
            get
            {

                //if (string.IsNullOrEmpty(predominio_s_r_en)) return string.Empty;

                if (edad_anos > 3)
                {
                    return (seq_pqrs_evaluacion_grado == 1) ? "Patológico" : "DLN";
                }

                return "Patológico";

            }
        }
        public override string FrecuenciaCardiaca_dx
        {
            get
            {
                if (ritmo.ToLower() == "irregular") return string.Empty;

             
                    if (FrecuenciaCardiaca_ComplejoQRS <= 60) return "Bradicardia";
                    if (FrecuenciaCardiaca_ComplejoQRS >= 100) return "Taquicardia";
               
                return "Normal";
            }
        }

        public override string seq_pqrs_todos_complejo_comentarios => (seq_pqrs_todos_complejo)
            ? "ATENCIÓN: ausencia de onda P antes de cada QRS"
            : string.Empty;

        public override string amplitud_r_v1_v2_dx => (amplitud_r_v1_v2 > 12.1) 
            ? "Aumentado" 
            : "Dentro de límites normales";

        public override string amplitud_r_v1_v2_dx2
        {
            get
            {
                if (amplitud_r_v1_v2_dx != "Aumentado") return string.Empty;
                return (eje != "+90º y +-180º") ? string.Empty : "Hipertrofia VD?";
            }
        }
        public override string amplitud_s_v1_v2_dx => (amplitud_s_v1_v2 > 25.4) 
            ? "Aumentado" 
            : "Dentro de límites normales";

        public override string amplitud_s_v1_v2_dx2
        {
            get
            {
                if (amplitud_s_v1_v2_dx != "Aumentado") return string.Empty;
                return (eje != "0º y -90º") ? string.Empty : "Hipertrofia VI?";
            }
        }

        public override string amplitud_r_v5_v6_dx => (amplitud_r_v5_v6 > 25.4)
             ? "Aumentado"
            : "Dentro de límites normales";


        public override string amplitud_r_v5_v6_dx2
        {
            get
            {
                if (amplitud_r_v5_v6_dx != "Aumentado") return string.Empty;
                return (eje != "0º y -90º") ? string.Empty : "Hipertrofia VI?";
            }

        }

        public override string amplitud_s_v5_v6_dx => (amplitud_s_v5_v6 > 3.9)
           ? "Aumentado"
            : "Dentro de límites normales";

        public override string amplitud_s_v5_v6_dx2
        {
            get
            {
                if (amplitud_s_v5_v6_dx != "Aumentado") return string.Empty;
                return (eje != "+90º y +-180º") ? string.Empty : "Hipertrofia VD?";
            }
        }


        #region supradesnivel_st

        public override string supradesnivel_st_en
        {

            get => $"Derivaciones adonde se lo observa: {_supradesnivel_st_en} {Caras.FirstOrDefault(c => c.ubicacion == _supradesnivel_st_en).cara} ";
            set => _supradesnivel_st_en = value;

        }

        public override string supradesnivel_st_Observacion
        {
            get
            {
                if (pediatrico == false)
                {
                    return (_supradesnivel_st_en == "en todas")
                        ? "Puede deberse a: repolarización precoz, pericarditis aguda u otras causas (miocarditis, sobrecargas ventriculares diastólicas, hiperkalemia)"
                        : "Descartar evento coronario agudo (injuria subepicárdica)";
                }

                return string.Empty;
            }
        }
        public override string supradesnivel_st_recomendacion =>
            (supradesnivel_st) ? "Correlacionar con la clínica del paciente" : string.Empty;

        #endregion

        #region infradesnivel_st
        public override string infradesnivel_st_text => (infradesnivel_st)
                ? "si (mayor de 1,5 mm)"
                : "no (menor de 1,5 mm)";
           
        

        public override string infradesnivel_st_en
        {
            get => $"Derivaciones adonde se lo observa: {_infradesnivel_st_en} {Caras.FirstOrDefault(c => { return _infradesnivel_st_en != null && c.ubicacion == _infradesnivel_st_en; })?.cara: string.em}";
            set => _infradesnivel_st_en = value;
        }

        public override string infradesnivel_st_Observacion
        {
            get
            {
                try
                {

                    if (pediatrico == false)
                    {
                        return (_infradesnivel_st_en.ToLower() == "en todas")
                            ? "Descartar sobrecargas sistólicas ventriculares e hipokalemia"
                            : ((Caras.Single(c => c.ubicacion == _infradesnivel_st_en).valor == 1)
                                ? "Posible evento coronario (injuria subendocárdica) o sobrecargas ventriculares sistólicas"
                                : "Posible evento coronario (injuria subendocárdica)");
                    }

                    return string.Empty;

                }
                catch (Exception e)
                {
                    return string.Empty;
                }
            }
        }

        public override string infradesnivel_st_recomendacion =>
            (pediatrico == false && infradesnivel_st) ? "Correlacionar con la clínica del paciente" : string.Empty;

        #endregion

    }
    public abstract class InformeEcgModel
    {

        #region Generales

        [Key]
        public int ecg_id { get; set; }
        public DateTime fecha_estudio { get; set; }
        public string digitado_por { get; set; }
        public bool pediatrico { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }

        public string fullName => $"{nombre.Trim()} {apellidos.Trim()}";
        public DateTime fecha_nacimiento { get; set; }

        public decimal totalmeses
        {
            get
            {

                var meses = (Convert.ToDecimal(fecha_estudio.Subtract(fecha_nacimiento).Days) / 365) * 12;

                return decimal.Round(meses, 0);
            }
        }

        public decimal edad => decimal.Round(decimal.Parse(fecha_estudio.Subtract(fecha_nacimiento).Days.ToString()) / 365, 1);

        public int edad_anos => (fecha_estudio.Subtract(fecha_nacimiento).Days) / 365;

        public int edad_meses => (12) - Math.Abs(fecha_estudio.Month - fecha_nacimiento.Month);

        public int edad_dias => fecha_estudio.Subtract(fecha_nacimiento).Days;

        public int motivo_id { get; set; }
        public string motivo { get; set; }

        #endregion

        #region Onda P

        public int ritmo_id { get; set; }
        public string ritmo { get; set; }
        public string deriv_di_ondap { get; set; }
        public string deriv_avf_ondap { get; set; }
        public bool ondap_antes_qrs { get; set; }
        public bool seq_pqrs_todos_complejo { get; set; }

        public abstract string seq_pqrs_todos_complejo_comentarios { get; }

        public int seq_pqrs_evaluacion_grado => (qrs_isodifasico_grado == "0°" ||
                                                 qrs_isodifasico_grado == "-30°" ||
                                                 qrs_isodifasico_grado == "-60°" ||
                                                 qrs_isodifasico_grado == "-90°" ||
                                                 qrs_isodifasico_grado == "-120°" ||
                                                 qrs_isodifasico_grado == "-150°" ||
                                                 qrs_isodifasico_grado == "+-180°" ||
                                                 qrs_isodifasico_grado == "+150°")
            ? 1
            : 0;

        public abstract string seg_pqrs_evalucion { get; }

        private double _duracion_ondaP = 0;
        public double duracion_ondap { get => _duracion_ondaP * 0.04; set => _duracion_ondaP = value; }

        public string duracion_ondap_dx
        {
            get
            {
                var _duracion = duracion_ondap;

                if (totalmeses <= 12)
                {
                    return (_duracion <= 0.07) ? "Normal" : "Patológico (si es en V1 HAI?)";
                }

                return (_duracion <= 0.09) ? "Normal" : "Patológico (si es en V1 HAI?)";
            }

        }

        public string duracion_ondap_dx2
        {
            get
            {
                var _duracion = duracion_ondap;

                if (totalmeses >= 12)
                {
                    return (_duracion <= 0.09) ? "Normal" : "Patológico (HVI?)";
                }

                return string.Empty;
            }

        }

        public string amplitud_ondap_dx => (amplitud_ondap <= 3) ? "Normal" : "Patológico (HAD?)";
        public int amplitud_ondap { get; set; }

        private double _intervalo_pr;
        public double intervalo_pr
        {
            get => _intervalo_pr * 0.04;
            set => _intervalo_pr = value;
        }

        public bool observa_aleteo_auricular { get; set; }
        public string observa_aleteo_auricular_comentario => (observa_aleteo_auricular)
            ? "ATENCIÓN: se observan ondas de aleteo auricular"
            : string.Empty;

        public rangoPR intervaloPr_rango
        {
            get
            {
                IEnumerable<rangoPR> rango = new List<rangoPR>()
                {
                    new rangoPR(){edad =0, inferior =0.08,superior =0.15},
                new rangoPR() { edad = 1, inferior = 0.08, superior = 0.16 },
                new rangoPR() { edad = 2, inferior = 0.08, superior =0.15 },
                new rangoPR() { edad = 3, inferior = 0.1, superior = 0.16 },
                new rangoPR() { edad = 4, inferior = 0.1, superior = 0.16 },
                new rangoPR() { edad = 5, inferior = 0.1, superior = 0.16 },
                new rangoPR() { edad = 6, inferior = 0.1, superior = 0.16 },
                new rangoPR() { edad = 7, inferior = 0.1, superior = 0.16 },
                new rangoPR() { edad = 8, inferior = 0.1, superior = 0.17 },
                new rangoPR() { edad = 9, inferior = 0.1, superior = 0.17 },
                new rangoPR() { edad = 10, inferior = 0.1, superior = 0.17 },
                new rangoPR() { edad = 11, inferior = 0.1, superior = 0.17 },
                new rangoPR() { edad = 9, inferior = 0.1, superior = 0.17 },
                new rangoPR() { edad = 10, inferior = 0.1, superior = 0.17 },
                new rangoPR() { edad = 11, inferior = 0.1, superior = 0.17 }

            };

                //TODO: Me quede Aqui.
                var _rango = rango.FirstOrDefault(r => r.edad == edad_anos);

                //if (edad_anos < 1)
                //{
                //    rango.inferior = double.Parse("0.08");
                //    rango.superior = double.Parse("0.15");
                //}

                return _rango;
            }


        }
        public string intervalo_pr_comentario
        {
            get
            {
                switch (intervalor_pr_dx)
                {

                    case "Corto":
                        return
                            "Posibles causas: Sme de preexitación (WPW), enf. por almacenamiento de glucógeno, etc.";

                    case "Largo":
                        return "Posibles causas: Bloqueo AV 1er grado, algunas miocarditis, Anomalía de Ebstein, intoxicación digital, hiperpotasemia, hipertonía vagal y deportistas entrenados, etc.";

                    default:
                        return string.Empty;
                }

            }

        }

        public class rangoPR
        {
            public int edad { get; set; }
            public double inferior { get; set; }
            public double superior { get; set; }

        }

        public string intervalor_pr_dx
        {
            get
            {
                DateTime _fechaInitialize = new DateTime(0001, 01, 01);
                if (fecha_nacimiento.Date == _fechaInitialize) return string.Empty;
                
                //var intervalorPR = intervalo_pr ;
                if (pediatrico) { 
                if (intervalo_pr == 0) return string.Empty;

                if (intervalo_pr < intervaloPr_rango.inferior) return "Corto";
                return intervalo_pr > intervaloPr_rango.superior ? "Largo" : "Normal";

                } else
                {
                    if (intervalo_pr < 0.1) return "Corto";
                    return intervalo_pr > 0.2 ? "Largo" : "Normal";
                }
                               
            }

        }

        #endregion

        #region Qrs

        public string deriv_di_qrs_r_u_s { get; set; }
        public string deriv_avf_qrs_r_u_s { get; set; }
        public string eje
        {
            get
            {

                switch (deriv_di_qrs_r_u_s)
                {
                    case "R" when deriv_avf_qrs_r_u_s == "R":
                        return "0º y +90º";

                    case "S" when deriv_avf_qrs_r_u_s == "S":
                        return "+-180º y -90º";
                    case "R" when deriv_avf_qrs_r_u_s == "S":
                        return "0º y -90º";
                    case "S" when deriv_avf_qrs_r_u_s == "R":
                        return "+90º y +-180º";
                    default:
                        return "--";
                }


            }
        }

        public abstract string eje_comentario { get; }

        public string eje_ondap
        {
            get
            {
                switch (deriv_di_ondap)
                {

                    case "+" when deriv_avf_ondap == "+":
                        return "0º y +90º";
                    case "+" when deriv_avf_ondap == "-":
                        return "0º y -90º";
                    case "-" when deriv_avf_ondap == "-":
                        return "+-180º y -90º";
                    case "-" when deriv_avf_ondap == "+":
                        return "+90º y +-180º";
                    default:
                        return "--";
                }
            }
        }

        public string eje_ondap_comentario
        {
            get
            {
                switch (deriv_di_ondap)
                {

                    case "+" when deriv_avf_ondap == "+":
                        return "Normal";

                    default:
                        return "Patológico";
                }

            }
        }
        public string comentario_ondap
        {
            get
            {
                switch (ondap_antes_qrs)
                {

                    case false:
                        return "Atención: no se observan ondas P idénticas antes de cada complejo QRS";

                    default:
                        return string.Empty;
                }

            }
        }

        // public string comentario_amplitudondap { get; set; }
        public bool complejos_qrs { get; set; }

        public double FrecuenciaCardiaca_ComplejoQRS => (r_r > 0) ? (1500 / (r_r / 0.04)) : 0;

        //public string FrecuenciaCardiaca_OndaP => () ? (1500/ 2) : "--";
        public abstract string FrecuenciaCardiaca_dx { get; }

        public string FrecuenciaCardiaca_Comentario => (ritmo.ToLower() == "irregular")
            ? "Medición ECG de la frecuencia cardíaca no confiable por rítmo irregular"
            : string.Empty;
        public int morfologiaqrs_id { get; set; }
        public string morfologia_qrs { get; set; }

        public string morfologia_qrs_comentario
        {
            get
            {
                if (morfologia_qrs == null) return string.Empty;

                if (morfologia_qrs.ToLower() != "normal")
                {
                    if (intervalor_pr_dx == "Corto")
                    {
                        return "Síndrome de preexitación?";
                    }

                    return morfologia_qrs_comentario2;

                }
                return String.Empty;
                //= SI($D$23 = "rSR´", SI(ECG!$F$35 = "Prolongado", SI(P27 > 0, "Si se observa en V1 - V2, puede ser bloqueo de rama derecha", ""), ""), "")
                //= SI(E37 = "", "", SI(E37 = "Normal", "", SI(F19 = "Corto", "Síndrome de preexitación?", 'Ingreso datos'!M25 & 'Ingreso datos'!M26 & 'Ingreso datos'!M27)))
            }
        }

        private string morfologia_qrs_comentario2
        {
            get
            {
                String comentario = null;

                if (string.IsNullOrEmpty(morfologia_qrs) || morfologia_qrs.ToLower() == "normal") return string.Empty;

                if (intervalor_pr_dx.ToLower() == "corto") return "Síndrome de preexitación?";


                if (morfologia_qrs.ToLower() == "rsr'" && duracion_qrs_dx == "Prolongado")
                {
                    var ejeEntero = (eje_comentario == "+90º y +-180º") ? 1 : 0;

                    if (ejeEntero > 0) comentario = "Si se observa en V1 - V2, puede ser bloqueo de rama derecha";

                }

                var morfologiaEntero = (morfologia_qrs.ToLower() == "s ancha") ? 1 : 0;

                if (morfologiaEntero > 0)
                {
                    comentario += "Si se ven en V5 y V6 sospechar bloqueo completo de rama derecha";
                }

                var morfologiaEntero2 = (morfologia_qrs == "r empastada y ancha") ? 1 : 0;
                if (morfologiaEntero2 > 0)
                    comentario += "En ausencia de Q en DI, aVL, V5 y V6, sospechar bloqueo completo de rama izquierda";

                return comentario;


            }
        }

        private double _duracion_ondaQrs;
        public double duracion_qrs
        {
            get => _duracion_ondaQrs * 0.04;
            set => _duracion_ondaQrs = value;
        }

        public string duracion_qrs_dx => (duracion_qrs > duracion_qrs_valores) ? "Prolongado" : "Normal";

        private double duracion_qrs_valores
        {
            get
            {
                if (edad_anos > 16 || edad_anos >= 12) return 0.1;
                if (edad_anos >= 8) return 0.09;
                return (edad_anos >= 3) ? 0.08 : 0.07;


            }
        }

        public string duracion_qrs_comentario => (duracion_qrs_dx == "Prolongado")
        ? "Posibles causas: alteraciones de la conducción ventricular (preexitación, bloqueo intraventricular, arritmias ventriculares), dilatación ventricular o bloqueos de rama, etc.  Rara vez por hipertrofia"
        : string.Empty;

        public int amplitud_r_v1_v2 { get; set; }
        public abstract string amplitud_r_v1_v2_dx {get; }
        public abstract string amplitud_r_v1_v2_dx2 {get;} 

        public int amplitud_s_v1_v2 { get; set; }
        public abstract string amplitud_s_v1_v2_dx { get; }
        public abstract string amplitud_s_v1_v2_dx2  {get;}
        public int amplitud_r_v5_v6 { get; set; }
        public abstract string amplitud_r_v5_v6_dx { get; } 

        public abstract string amplitud_r_v5_v6_dx2 { get; }
       

        public int amplitud_s_v5_v6 { get; set; }
        public abstract string amplitud_s_v5_v6_dx { get; }

        public abstract string amplitud_s_v5_v6_dx2 { get; }
         
        public string qrs_isodifasico_en { get; set; }

        public string derivacionPerpendicular_en
        {
            get
            {
                switch (qrs_isodifasico_en)
                {
                    case "DI":
                        return "aVF";
                    case "DII":
                        return "aVL";
                    case "DIII":
                        return "aVR";
                    case "aVF":
                        return "DI";
                    case "aVR":
                        return "DIII";
                    case "aVL":
                        return "DII";
                    default:
                        return "--";
                }
            }
        }

        public string qrs_isodifasico_grado
        {
            get
            {
                if (predominio_s_r_en == "R")
                {
                    switch (qrs_isodifasico_en)
                    {
                        case "DI" when derivacionPerpendicular_en == "aVF":
                            return "+90°";
                        case "DII" when derivacionPerpendicular_en == "aVL":
                            return "-30°";
                        case "DIII" when derivacionPerpendicular_en == "aVR":
                            return "-150°";
                        case "aVF" when derivacionPerpendicular_en == "DI":
                            return "0°";
                        case "aVR" when derivacionPerpendicular_en == "DIII":
                            return "+120°";
                        case "aVL" when derivacionPerpendicular_en == "DII":
                            return "+60°";
                        default:
                            return "--";
                    }
                }
                else
                {
                    switch (qrs_isodifasico_en)
                    {
                        case "DI" when derivacionPerpendicular_en == "aVF":
                            return "-90°";
                        case "DII" when derivacionPerpendicular_en == "aVL":
                            return "+150°";
                        case "DIII" when derivacionPerpendicular_en == "aVR":
                            return "+30°";
                        case "aVF" when derivacionPerpendicular_en == "DI":
                            return "+-180°";
                        case "aVR" when derivacionPerpendicular_en == "DIII":
                            return "-60°";
                        case "aVL" when derivacionPerpendicular_en == "DII":
                            return "-120°";
                        default:
                            return "--";
                    }
                }


            }
        }
        public string predominio_s_r_en { get; set; }
        public string predominio_s_r_val { get; set; }
        
        #endregion
        
        #region QTST   
        
        private double _intervalo_qt;
        public double intervalo_qt
        {
            get => _intervalo_qt * 0.04;
            set => _intervalo_qt = value;
        }

        public double intervalo_qt_bazer => (intervalo_qt != 0) ? Math.Round(intervalo_qt / Math.Sqrt(r_r),2):0;

        public string intervalo_qt_bazer_dx
        {
            get
            {
                var valores = (totalmeses >= 192) ? 0.425: (totalmeses>=24)?0.44:0.45;

                return (intervalo_qt_bazer > valores) ? "Prolongado" : string.Empty;
            }
        }

        public string intervalo_qt_bazer_Comentario => (intervalo_qt_bazer_dx == "Prolongado")
            ? "Posibles causas: hipocalcemia, miocarditis, Sme.del QT© largo congénito o adquirido, TEC, fármacos (macrólidos, quinolonas, etc.), etc"
            : string.Empty;
        
        private double _rr;
        public double r_r
        {
            get => _rr * 0.04;
            set => _rr = value;
        }

        public bool _stIsoelectrico;
        public string st_isoelectrico
        {
            get => (_stIsoelectrico)? "Si (o menos de 1 mm)" : "No (o más de 1 mm)";
            //set => _stIsoelectrico = value;
        }

        public string st_isoelectrico_comentario => (st_isoelectrico == "No (o más de 1 mm)")
            ? "A excepción de variantes normales en adolescentes sanos (si la elevación del segmento ST es menor de 4 mm en derivaciones V4 a V6 e inferiores (I, III y AVF)), sospechar pericarditis, isquemia miocárdica, hipertrofia ventricular izquierda o derecha graves, efecto digitálico, miocarditis o alteraciones hidroelectrolíticas"
            : string.Empty;

        private string _onda_t_v1_v2;
        public string onda_t_v1_v2
        {
            get => (_onda_t_v1_v2 == "+") ? "Positiva" : "Negativa";
            set => _onda_t_v1_v2 = value;
        }

        public string onda_t_v1_v2_comentario
        {
            get
            {
                if (onda_t_v1_v2 == "Negativa")
                {
                    return (amplitud_r_v1_v2_dx == "Aumentado (supera el p98 para edad)") ? "Puede representar patrón de sobrecarga derecha" : "Puede ser normal en niños";
                }

                return string.Empty;
            }
        }

        private string _onda_t_v5_v6;

        public string onda_t_v5_v6
        {
            get => (_onda_t_v5_v6 == "+") ? "Positiva" : "Negativa";
            set => _onda_t_v5_v6 = value;
        }

        public string onda_t_v5_v6_comentario => (onda_t_v5_v6 == "Negativa")
            ? "Pueden sugerir hipertrofia ventricular izquierda con patron de sobrecarga (si son asimétricas)  o isquemia miocárdica (si son simétricas)"
            : string.Empty;

        #endregion

        #region Onda Q
        
        public bool presencia_onda_q { get; set; }

        public string presencia_onda_q_comentario
        {
            get
            {
                if(!presencia_onda_q) return string.Empty;

                if (pico_mas_alto_en_dx == "Anormal" || duracion_ondaq_dx == "Patológico" ||
                    profundida_ondaq_dx == "Patológico")
                    return "Onda Q patológica sugiere hipertrofia ventricular (izquierda), post IAM, otras causas";

                return string.Empty;

            }
        }

        public string DescarteExtraSistolia => (complejos_qrs) ? "Descartar extrasistolia" : string.Empty;

        public string extraSistolia
        {
            get
            {
                if (!presencia_onda_q) return string.Empty;

                if (profundida_ondaq == 0) return string.Empty;

                var _evaluaOndaQ = (profundida_ondaq >= valoresComparacionOndaQ) ? "Patológico" : "Adecuado para la edad";

                if (_evaluaOndaQ == "Patológico") return string.Empty;
                if (duracion_ondaq_dx == "Patológico") return string.Empty;
                return pico_mas_alto_en_dx != "Anormal" ? _evaluaOndaQ : string.Empty;
            }
        }

        public string BloqueodeRama
        {
            get
            {
                if (duracion_qrs == 0) return string.Empty;
                if (morfologiaqrs_id != 2) return string.Empty;
                if (duracion_qrs >= 0.12) return "Impresiona bloqueo completo de rama derecha";
                return duracion_qrs >= 0.1 ? "Impresiona bloqueo incompleto de rama derecha" : string.Empty;
            }
        }

        public string A_descartar_CIA
        {
            get
            {
                if (BloqueodeRama != "Impresiona bloqueo incompleto de rama derecha") return string.Empty;
                 return (motivo_id == 2) ? "Descartar CIA si se observa ese patrón de QRS en V1    -" : string.Empty;
                //= SI(F36 = "", "", SI(F36 = "Impresiona bloqueo incompleto de rama derecha", SI(B3 = "Hallazgo de soplo cardíaco", "Descartar CIA si se observa ese patrón de QRS en V1    -", ""), ""))
            }
        }

        private double valoresComparacionOndaQ
        {
            get
            {
                switch (pico_mas_alto_en.ToLower())
                {
                    case "avl":
                        return 2;
                    case "d1":
                        return 3;
                    case "d2":
                        return 4;
                    case "avf":
                        return 4;
                    case "d3":
                        if (edad_anos >= 8 && edad_anos <= 15) return 3;
                        if (edad_anos >= 5 && edad_anos <= 7) return 4;
                        if (edad_anos >= 3 && edad_anos <= 4) return 5;
                        if (edad_anos >= 1 && edad_anos <= 2) return 6;
                        break;
                    case "v6":
                        if (edad_anos >= 8 && edad_anos <= 15) return 3;
                        if (edad_anos >= 5 && edad_anos <= 7) return 4.5;
                        if (edad_anos >= 3 && edad_anos <= 4) return 3.5;
                        if (edad_anos >= 1 && edad_anos <= 2) return 3;
                        break;
                        

                }

                return 0;
            }
        }

        private string _pico_mas_alto_en;
        /// <summary>
        /// Derivacion mas Pronunciada.
        /// </summary>
        public string pico_mas_alto_en { get=> (presencia_onda_q)? _pico_mas_alto_en : String.Empty;
            set => _pico_mas_alto_en = value;
        }

        public string pico_mas_alto_en_dx
        {
            get
            {
                if(!presencia_onda_q) return string.Empty;

                if (pico_mas_alto_en.ToLower() == "avr" ||
                    pico_mas_alto_en.ToLower() == "v1" ||
                    pico_mas_alto_en.ToLower() == "v2" ||
                    pico_mas_alto_en.ToLower() == "v3" ||
                    pico_mas_alto_en.ToLower() == "v4") return "Anormal";

                return string.Empty;
            }
        }

        private int _profundida_ondaq;

        public int profundida_ondaq
        {
            get => (presencia_onda_q) ? _profundida_ondaq : 0;
            set => _profundida_ondaq = value;

        }

        public string profundida_ondaq_dx
        {
            get
            {
                if (!presencia_onda_q) return string.Empty;

                if (presencia_onda_q == false ) return string.Empty;
                if (extraSistolia != string.Empty) return string.Empty;
                if (pico_mas_alto_en_dx == "Anormal") return string.Empty;
                return duracion_ondaq_dx == "Patológico" ? string.Empty : extraSistolia;
            }
        }

        public double _duracion_ondaQ;
        public double duracion_ondaq
        {
            get => (presencia_onda_q) ?  _duracion_ondaQ * 0.04 : 0;
            set => _duracion_ondaQ = value;
        }

        public string duracion_ondaq_dx => (!presencia_onda_q) ? string.Empty: (duracion_ondaq >= 0.03) ? "Patológico" : string.Empty;

        #endregion

        #region ST

        #region supradesnivel

        public bool supradesnivel_st { get; set; }
        public string supradesnivel_st_text => (supradesnivel_st)
            ? "si (mayor de 1,5 mm)"
            : "no (menor de 1,5 mm)";

        public string _supradesnivel_st_en;
        public abstract string supradesnivel_st_en { get; set; }

        public abstract string supradesnivel_st_Observacion { get; }
      

        public abstract string supradesnivel_st_recomendacion { get;}


        #endregion


        #region Infradesnivel

        public bool infradesnivel_st { get; set; }
        public abstract string infradesnivel_st_text { get; }

        public string _infradesnivel_st_en;
        public abstract string infradesnivel_st_en { get; set; }

        public abstract string infradesnivel_st_Observacion { get; }


        //"en todas",,SI(ST!A7=1,"Posible evento coronario (injuria subendocárdica) o sobrecargas ventriculares sistólicas","Posible evento coronario (injuria subendocárdica)"
        public abstract string infradesnivel_st_recomendacion { get; }

        #endregion



        public IEnumerable<SegmentoST> Caras
        {
            get
            {
                IEnumerable<SegmentoST> segmentos = new List<SegmentoST>()
                {
                    new SegmentoST(){ubicacion = "DII - DIII y AVF",cara = "cara inferior", valor = 0 },
                    new SegmentoST(){ubicacion = "V1 - V2",cara = "cara septal", valor = 0 },
                    new SegmentoST(){ubicacion = "V1 - V2 - V3 - V4",cara = "cara anterior", valor = 0 },
                    new SegmentoST(){ubicacion = "V5 - V6",cara = "cara lateral baja", valor = 1 },
                    new SegmentoST(){ubicacion = "DI - AVL",cara = "cara lateral alta", valor = 1 },
                    new SegmentoST(){ubicacion = "V1 a V6",cara = "anterior extenso", valor = 0 },
                    new SegmentoST(){ubicacion = "DI - AVL – V5 y V6",cara = "antero lateral", valor = 1 },
                    new SegmentoST(){ubicacion = "en todas", cara= string.Empty, valor = 0}
                };

                return segmentos;
            }
        }


        #endregion

    }

    public class SegmentoST
    {
        public string ubicacion { get; set; }
        public string cara { get; set; }

        public int valor { get; set; }
    }
}