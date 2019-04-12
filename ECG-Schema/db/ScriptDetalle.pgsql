CREATE SEQUENCE public.ecg_ondap_seq INCREMENT 1 MINVALUE 1 START 1;
CREATE TABLE public.ecg_ondap(
    ecg_ondap_id NUMERIC DEFAULT nextval('ecg_ondap_seq')  NOT NULL,
    ecg_id NUMERIC,
    ritmo_id NUMERIC,
    deriv_di_ondap CHARACTER(1),
    deriv_avf_ondap CHARACTER(1),
    ondap_antes_qrs BOOLEAN,
    seq_pqrs_todos_complejo BOOLEAN,
    duracion_ondap NUMERIC,
    amplitud_ondap NUMERIC,
    intervalo_pr NUMERIC,
     CONSTRAINT pk_ecg_ondap PRIMARY KEY (ecg_ondap_id)
);

CREATE SEQUENCE public.ecg_qrs_seq INCREMENT 1 MINVALUE 1 START 1;
CREATE TABLE public.ecg_qrs(
    ecg_qrs_id NUMERIC DEFAULT nextval('ecg_qrs_seq')  NOT NULL,
    ecg_id NUMERIC,
    deriv_di_qrs_r_u_s CHARACTER(1),
    deriv_avf_qrs_r_u_s CHARACTER(1),
    complejos_qrs BOOLEAN,
    morfologiaqrs_id NUMERIC,
    duracion_qrs CHARACTER(40),
    amplitud_r_v1_v2 NUMERIC,
    amplitud_s_v1_v2 NUMERIC,
    amplitud_r_v5_v6 NUMERIC,
    amplitud_s_v5_v6 NUMERIC,
    qrs_isodifasico_en CHARACTER(40),
    predominio_s_r_avf CHARACTER(1),
    predominio_s_r_avl CHARACTER(1),
    predominio_s_r_avr CHARACTER(1),
    predominio_s_r_diii CHARACTER(1),
    predominio_s_r_dii CHARACTER(1),
    predominio_s_r_di CHARACTER(1),
     CONSTRAINT pk_ecg_qrs PRIMARY KEY (ecg_qrs_id)
);

CREATE SEQUENCE public.ecg_qtst_seq INCREMENT 1 MINVALUE 1 START 1;
CREATE TABLE public.ecg_qtst(
     ecg_qtst_id NUMERIC DEFAULT nextval('ecg_qtst_seq')  NOT NULL,
    ecg_id NUMERIC,
    intervalo_qt NUMERIC,
    r_r NUMERIC,
    st_isoelectrico BOOLEAN,
    onda_t_v1_v2 CHARACTER(1),
    onda_t_v5_v6 CHARACTER(1),
     CONSTRAINT pk_ecg_qtst PRIMARY KEY (ecg_qtst_id)
);

CREATE SEQUENCE public.ecg_ondaq_seq INCREMENT 1 MINVALUE 1 START 1;
CREATE TABLE public.ecg_ondaq(
     ecg_ondaq_id NUMERIC DEFAULT nextval('ecg_ondaq_seq')  NOT NULL,
    ecg_id NUMERIC,
    presencia_onda_q BOOLEAN,
    pico_mas_alto_en CHARACTER(5),
    profundida_ondaq NUMERIC,
    duracion_ondaq NUMERIC,
     CONSTRAINT pk_ecg_ondaq PRIMARY KEY (ecg_ondaq_id)
);

CREATE SEQUENCE public.ecg_st_seq INCREMENT 1 MINVALUE 1 START 1;
CREATE TABLE public.ecg_st (
    ecg_st_id NUMERIC DEFAULT nextval('ecg_st_seq')  NOT NULL,
    ecg_id NUMERIC,
    supradesnivel_st BOOLEAN,
    supradesnivel_st_en CHARACTER(40),
    infradesnivel_st BOOLEAN,
    infradesnivel_st_en CHARACTER(40),
    CONSTRAINT pk_ecg_st PRIMARY KEY (ecg_st_id)
);