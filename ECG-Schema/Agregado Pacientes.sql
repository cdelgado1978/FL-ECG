/* ---------------------------------------------------------------------- */
/* Script generated with: DeZign for Databases V8.1.2                     */
/* Target DBMS:           PostgreSQL 9                                    */
/* Project file:          ECG-Schema.dez                                  */
/* Project name:                                                          */
/* Author:                                                                */
/* Script type:           Alter database script                           */
/* Created on:            2018-08-23 00:05                                */
/* ---------------------------------------------------------------------- */


/* ---------------------------------------------------------------------- */
/* Drop foreign key constraints                                           */
/* ---------------------------------------------------------------------- */

ALTER TABLE public.ecg_detalle DROP CONSTRAINT ecg_ritmo_ecg_detalle;

ALTER TABLE public.ecg_detalle DROP CONSTRAINT morfologia_qrs_ecg_detalle;

ALTER TABLE public.ecg_detalle DROP CONSTRAINT ecg_ecg_detalle;

/* ---------------------------------------------------------------------- */
/* Alter table "public.ecg_detalle"                                       */
/* ---------------------------------------------------------------------- */

ALTER TABLE public.ecg_detalle ALTER COLUMN duracion_qrs TYPE NUMERIC;

/* ---------------------------------------------------------------------- */
/* Add foreign key constraints                                            */
/* ---------------------------------------------------------------------- */

ALTER TABLE public.ecg_detalle ADD CONSTRAINT ecg_ecg_detalle 
    FOREIGN KEY (ecg_id) REFERENCES public.ecg (ecg_id);

ALTER TABLE public.ecg_detalle ADD CONSTRAINT ecg_ritmo_ecg_detalle 
    FOREIGN KEY (ritmo_id) REFERENCES public.ecg_ritmo (ritmo_id);

ALTER TABLE public.ecg_detalle ADD CONSTRAINT morfologia_qrs_ecg_detalle 
    FOREIGN KEY (morfologiaqrs_id) REFERENCES public.morfologia_qrs (morfologiaqrs_id);
