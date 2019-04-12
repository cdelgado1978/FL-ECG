/* ---------------------------------------------------------------------- */
/* Script generated with: DeZign for Databases V8.1.2                     */
/* Target DBMS:           PostgreSQL 9                                    */
/* Project file:          ECG-Schema.dez                                  */
/* Project name:                                                          */
/* Author:                                                                */
/* Script type:           Database creation script                        */
/* Created on:            2018-08-16 10:06                                */
/* ---------------------------------------------------------------------- */


/* ---------------------------------------------------------------------- */
/* Add sequences                                                          */
/* ---------------------------------------------------------------------- */

CREATE SEQUENCE public.motivo_visita_seq INCREMENT 1 MINVALUE 0 START 0;

CREATE SEQUENCE public.ecg_seq INCREMENT 1 MINVALUE 0 START 0;

CREATE SEQUENCE public.ecg_detalle_seq INCREMENT 1 MINVALUE 0 START 0;

CREATE SEQUENCE public.morfologia_qrs_seq INCREMENT 1 MINVALUE 0 START 0;

CREATE SEQUENCE public.paciente_seq INCREMENT 1 MINVALUE 0 START 0;

CREATE SEQUENCE public.AspNetUserClaims_Id_seq INCREMENT 1 MINVALUE 0 START 0;

/* ---------------------------------------------------------------------- */
/* Add tables                                                             */
/* ---------------------------------------------------------------------- */

/* ---------------------------------------------------------------------- */
/* Add table "public.motivos_visita"                                      */
/* ---------------------------------------------------------------------- */

CREATE TABLE public.motivos_visita (
    motivo_id NUMERIC DEFAULT nextval('motivo_visita_seq')  NOT NULL,
    motivo CHARACTER(100)  NOT NULL,
    fecha_creacion DATE  NOT NULL,
    usuario CHARACTER(40)  NOT NULL,
    ultima_actualizacion DATE,
    usuario_ultima_actualizacion CHARACTER(40),
    activo BOOLEAN DEFAULT true  NOT NULL,
    CONSTRAINT pk_motivos_visita PRIMARY KEY (motivo_id)
);

CREATE INDEX idx_motivos_visita_1 ON public.motivos_visita (motivo_id,motivo);

/* ---------------------------------------------------------------------- */
/* Add table "public.paciente"                                            */
/* ---------------------------------------------------------------------- */

CREATE TABLE public.paciente (
    paciente_id NUMERIC DEFAULT nextval('paciente_seq')  NOT NULL,
    nombre CHARACTER(60)  NOT NULL,
    apellidos CHARACTER(60)  NOT NULL,
    fecha_nacimiento DATE  NOT NULL,
    activo BOOLEAN DEFAULT true  NOT NULL,
    fecha_creacion DATE  NOT NULL,
    creado_por CHARACTER(80),
    fecha_ultimamodificacion DATE,
    modificado_por CHARACTER(80),
    genero CHARACTER,
    CONSTRAINT pk_paciente PRIMARY KEY (paciente_id)
);

CREATE INDEX idx_paciente_1 ON public.paciente (paciente_id,nombre,apellidos,activo);

/* ---------------------------------------------------------------------- */
/* Add table "public.AspNetRoles"                                         */
/* ---------------------------------------------------------------------- */

CREATE TABLE public.AspNetRoles (
    Id CHARACTER VARYING(128)  NOT NULL,
    Name CHARACTER VARYING(256)  NOT NULL,
    CONSTRAINT AspNetRoles_pkey PRIMARY KEY (Id)
);

/* ---------------------------------------------------------------------- */
/* Add table "public.AspNetUsers"                                         */
/* ---------------------------------------------------------------------- */

CREATE TABLE public.AspNetUsers (
    Id CHARACTER VARYING(128)  NOT NULL,
    UserName CHARACTER VARYING(256)  NOT NULL,
    PasswordHash CHARACTER VARYING(256),
    SecurityStamp CHARACTER VARYING(256),
    Email CHARACTER VARYING(256) DEFAULT 'NULL',
    EmailConfirmed BOOLEAN DEFAULT false  NOT NULL,
    PhoneNumber CHARACTER VARYING(256),
    PhoneNumberConfirmed BOOLEAN DEFAULT false  NOT NULL,
    TwoFactorEnabled BOOLEAN DEFAULT false  NOT NULL,
    LockoutEndDateUtc TIMESTAMP,
    LockoutEnabled BOOLEAN DEFAULT false  NOT NULL,
    AccessFailedCount INTEGER DEFAULT 0  NOT NULL,
    CONSTRAINT AspNetUsers_pkey PRIMARY KEY (Id)
);

/* ---------------------------------------------------------------------- */
/* Add table "public.ecg"                                                 */
/* ---------------------------------------------------------------------- */

CREATE TABLE public.ecg (
    ecg_id NUMERIC DEFAULT nextval('ecg_seq')  NOT NULL,
    fecha_estudio DATE  NOT NULL,
    digitado_por CHARACTER(90),
    paciente_id NUMERIC,
    pediatrico BOOLEAN,
    motivo_id NUMERIC,
    CONSTRAINT pk_ecg PRIMARY KEY (ecg_id)
);

CREATE INDEX idx_ecg_1 ON public.ecg (ecg_id,fecha_estudio,digitado_por);

/* ---------------------------------------------------------------------- */
/* Add table "public.ecg_ritmo"                                           */
/* ---------------------------------------------------------------------- */

CREATE TABLE public.ecg_ritmo (
    ritmo_id NUMERIC  NOT NULL,
    ritmo CHARACTER(40)  NOT NULL,
    CONSTRAINT pk_ecg_ritmo PRIMARY KEY (ritmo_id)
);

/* ---------------------------------------------------------------------- */
/* Add table "public.morfologia_qrs"                                      */
/* ---------------------------------------------------------------------- */

CREATE TABLE public.morfologia_qrs (
    morfologiaqrs_id NUMERIC DEFAULT nextval('ecg_detalle_seq')  NOT NULL,
    morfologia_qrs CHARACTER(40)  NOT NULL,
    CONSTRAINT pk_morfologia_qrs PRIMARY KEY (morfologiaqrs_id)
);

/* ---------------------------------------------------------------------- */
/* Add table "public.AspNetUserClaims"                                    */
/* ---------------------------------------------------------------------- */

CREATE TABLE public.AspNetUserClaims (
    Id INTEGER DEFAULT nextval('"AspNetUserClaims_Id_seq"')  NOT NULL,
    ClaimType CHARACTER VARYING(256),
    ClaimValue CHARACTER VARYING(256),
    UserId CHARACTER VARYING(128)  NOT NULL,
    CONSTRAINT AspNetUserClaims_pkey PRIMARY KEY (Id)
);

/* ---------------------------------------------------------------------- */
/* Add table "public.AspNetUserLogins"                                    */
/* ---------------------------------------------------------------------- */

CREATE TABLE public.AspNetUserLogins (
    UserId CHARACTER VARYING(128)  NOT NULL,
    LoginProvider CHARACTER VARYING(128)  NOT NULL,
    ProviderKey CHARACTER VARYING(128)  NOT NULL,
    CONSTRAINT AspNetUserLogins_pkey PRIMARY KEY (UserId, LoginProvider, ProviderKey)
);

/* ---------------------------------------------------------------------- */
/* Add table "public.AspNetUserRoles"                                     */
/* ---------------------------------------------------------------------- */

CREATE TABLE public.AspNetUserRoles (
    UserId CHARACTER VARYING(128)  NOT NULL,
    RoleId CHARACTER VARYING(128)  NOT NULL,
    CONSTRAINT AspNetUserRoles_pkey PRIMARY KEY (UserId, RoleId)
);

/* ---------------------------------------------------------------------- */
/* Add table "public.ecg_detalle"                                         */
/* ---------------------------------------------------------------------- */

CREATE TABLE public.ecg_detalle (
    ecg_det_id NUMERIC DEFAULT nextval('ecg_detalle_seq')  NOT NULL,
    ecg_id NUMERIC,
    ritmo_id NUMERIC,
    deriv_di_ondap CHARACTER(1),
    deriv_avf_ondap CHARACTER(1),
    ondap_antes_qrs BOOLEAN,
    seq_pqrs_todos_complejo BOOLEAN,
    duracion_ondap NUMERIC,
    amplitud_ondap NUMERIC,
    intervalo_pr NUMERIC,
    deriv_di_qrs_r_u_s CHARACTER(1),
    deriv_avf_qrs_r_u_s CHARACTER(1),
    complejos_qrs BOOLEAN,
    morfologiaqrs_id NUMERIC,
    duracion_qrs CHARACTER(40),
    amplitud_r_v1_v2 NUMERIC,
    amplitud_s_v1_v2 NUMERIC,
    amplitud_r_v5_v6 NUMERIC,
    amplitud_s_v5_v6 NUMERIC,
    qrs_isodif?co_en CHARACTER(40),
    predominio_s_r_avf CHARACTER(1),
    predominio_s_r_avl CHARACTER(1),
    predominio_s_r_avr CHARACTER(1),
    predominio_s_r_diii CHARACTER(1),
    predominio_s_r_dii CHARACTER(1),
    predominio_s_r_di CHARACTER(1),
    intervalo_qt NUMERIC,
    r_r NUMERIC,
    st_isoelectrico BOOLEAN,
    onda_t_v1_v2 CHARACTER(1),
    onda_t_v5_v6 CHARACTER(1),
    presencia_onda_q BOOLEAN,
    pico_mas_alto_en CHARACTER(5),
    profundida_ondaq NUMERIC,
    duracion_ondaq NUMERIC,
    supradesnivel_st BOOLEAN,
    supradesnivel_st_en CHARACTER(40),
    infradesnivel_st BOOLEAN,
    infradesnivel_st_en CHARACTER(40),
    CONSTRAINT pk_ecg_detalle PRIMARY KEY (ecg_det_id)
);

/* ---------------------------------------------------------------------- */
/* Add foreign key constraints                                            */
/* ---------------------------------------------------------------------- */

ALTER TABLE public.AspNetUserClaims ADD CONSTRAINT FK_AspNetUserClaims_AspNetUsers_User_Id 
    FOREIGN KEY (UserId) REFERENCES public.AspNetUsers (Id) ON DELETE CASCADE;

ALTER TABLE public.AspNetUserLogins ADD CONSTRAINT FK_AspNetUserLogins_AspNetUsers_UserId 
    FOREIGN KEY (UserId) REFERENCES public.AspNetUsers (Id) ON DELETE CASCADE;

ALTER TABLE public.AspNetUserRoles ADD CONSTRAINT FK_AspNetUserRoles_AspNetRoles_RoleId 
    FOREIGN KEY (RoleId) REFERENCES public.AspNetRoles (Id) ON DELETE CASCADE;

ALTER TABLE public.AspNetUserRoles ADD CONSTRAINT FK_AspNetUserRoles_AspNetUsers_UserId 
    FOREIGN KEY (UserId) REFERENCES public.AspNetUsers (Id) ON DELETE CASCADE;

ALTER TABLE public.ecg ADD CONSTRAINT paciente_ecg 
    FOREIGN KEY (paciente_id) REFERENCES public.paciente (paciente_id);

ALTER TABLE public.ecg ADD CONSTRAINT motivos_visita_ecg 
    FOREIGN KEY (motivo_id) REFERENCES public.motivos_visita (motivo_id);

ALTER TABLE public.ecg_detalle ADD CONSTRAINT ecg_ecg_detalle 
    FOREIGN KEY (ecg_id) REFERENCES public.ecg (ecg_id);

ALTER TABLE public.ecg_detalle ADD CONSTRAINT ecg_ritmo_ecg_detalle 
    FOREIGN KEY (ritmo_id) REFERENCES public.ecg_ritmo (ritmo_id);

ALTER TABLE public.ecg_detalle ADD CONSTRAINT morfologia_qrs_ecg_detalle 
    FOREIGN KEY (morfologiaqrs_id) REFERENCES public.morfologia_qrs (morfologiaqrs_id);
