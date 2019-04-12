/* ---------------------------------------------------------------------- */
/* Script generated with: DeZign for Databases V8.1.2                     */
/* Target DBMS:           PostgreSQL 9                                    */
/* Project file:          ECG-Schema.dez                                  */
/* Project name:                                                          */
/* Author:                                                                */
/* Script type:           Database drop script                            */
/* Created on:            2018-08-16 10:06                                */
/* ---------------------------------------------------------------------- */


/* ---------------------------------------------------------------------- */
/* Drop foreign key constraints                                           */
/* ---------------------------------------------------------------------- */

ALTER TABLE public.AspNetUserClaims DROP CONSTRAINT FK_AspNetUserClaims_AspNetUsers_User_Id;

ALTER TABLE public.AspNetUserLogins DROP CONSTRAINT FK_AspNetUserLogins_AspNetUsers_UserId;

ALTER TABLE public.AspNetUserRoles DROP CONSTRAINT FK_AspNetUserRoles_AspNetRoles_RoleId;

ALTER TABLE public.AspNetUserRoles DROP CONSTRAINT FK_AspNetUserRoles_AspNetUsers_UserId;

ALTER TABLE public.ecg DROP CONSTRAINT paciente_ecg;

ALTER TABLE public.ecg DROP CONSTRAINT motivos_visita_ecg;

ALTER TABLE public.ecg_detalle DROP CONSTRAINT ecg_ecg_detalle;

ALTER TABLE public.ecg_detalle DROP CONSTRAINT ecg_ritmo_ecg_detalle;

ALTER TABLE public.ecg_detalle DROP CONSTRAINT morfologia_qrs_ecg_detalle;

/* ---------------------------------------------------------------------- */
/* Drop table "public.ecg_detalle"                                        */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE public.ecg_detalle DROP CONSTRAINT pk_ecg_detalle;

DROP TABLE public.ecg_detalle;

/* ---------------------------------------------------------------------- */
/* Drop table "public.AspNetUserRoles"                                    */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE public.AspNetUserRoles DROP CONSTRAINT AspNetUserRoles_pkey;

DROP TABLE public.AspNetUserRoles;

/* ---------------------------------------------------------------------- */
/* Drop table "public.AspNetUserLogins"                                   */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE public.AspNetUserLogins DROP CONSTRAINT AspNetUserLogins_pkey;

DROP TABLE public.AspNetUserLogins;

/* ---------------------------------------------------------------------- */
/* Drop table "public.AspNetUserClaims"                                   */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE public.AspNetUserClaims DROP CONSTRAINT AspNetUserClaims_pkey;

DROP TABLE public.AspNetUserClaims;

/* ---------------------------------------------------------------------- */
/* Drop table "public.morfologia_qrs"                                     */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE public.morfologia_qrs DROP CONSTRAINT pk_morfologia_qrs;

DROP TABLE public.morfologia_qrs;

/* ---------------------------------------------------------------------- */
/* Drop table "public.ecg_ritmo"                                          */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE public.ecg_ritmo DROP CONSTRAINT pk_ecg_ritmo;

DROP TABLE public.ecg_ritmo;

/* ---------------------------------------------------------------------- */
/* Drop table "public.ecg"                                                */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE public.ecg DROP CONSTRAINT pk_ecg;

DROP TABLE public.ecg;

/* ---------------------------------------------------------------------- */
/* Drop table "public.AspNetUsers"                                        */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE public.AspNetUsers DROP CONSTRAINT AspNetUsers_pkey;

DROP TABLE public.AspNetUsers;

/* ---------------------------------------------------------------------- */
/* Drop table "public.AspNetRoles"                                        */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE public.AspNetRoles DROP CONSTRAINT AspNetRoles_pkey;

DROP TABLE public.AspNetRoles;

/* ---------------------------------------------------------------------- */
/* Drop table "public.paciente"                                           */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE public.paciente DROP CONSTRAINT pk_paciente;

DROP TABLE public.paciente;

/* ---------------------------------------------------------------------- */
/* Drop table "public.motivos_visita"                                     */
/* ---------------------------------------------------------------------- */

/* Drop constraints */

ALTER TABLE public.motivos_visita DROP CONSTRAINT pk_motivos_visita;

DROP TABLE public.motivos_visita;

/* ---------------------------------------------------------------------- */
/* Drop sequences                                                         */
/* ---------------------------------------------------------------------- */

DROP SEQUENCE public.motivo_visita_seq;

DROP SEQUENCE public.ecg_seq;

DROP SEQUENCE public.ecg_detalle_seq;

DROP SEQUENCE public.morfologia_qrs_seq;

DROP SEQUENCE public.paciente_seq;

DROP SEQUENCE public.AspNetUserClaims_Id_seq;
