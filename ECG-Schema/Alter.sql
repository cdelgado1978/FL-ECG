/* ---------------------------------------------------------------------- */
/* Script generated with: DeZign for Databases V8.1.2                     */
/* Target DBMS:           PostgreSQL 9                                    */
/* Project file:          ECG-Schema.dez                                  */
/* Project name:                                                          */
/* Author:                                                                */
/* Script type:           Alter database script                           */
/* Created on:            2018-08-08 14:38                                */
/* ---------------------------------------------------------------------- */


/* ---------------------------------------------------------------------- */
/* Add sequences                                                          */
/* ---------------------------------------------------------------------- */

CREATE SEQUENCE motivo_visita_seq INCREMENT 1 MINVALUE 0 START 0;

/* ---------------------------------------------------------------------- */
/* Add table "motivos_visita"                                             */
/* ---------------------------------------------------------------------- */

CREATE TABLE motivos_visita (
    motivo_id NUMERIC DEFAULT nextval('motivo_visita_seq')  NOT NULL,
    motivo CHARACTER(100)  NOT NULL,
    fecha_creacion DATE  NOT NULL,
    usuario CHARACTER(40)  NOT NULL,
    ultima_actualizacion DATE,
    usuario_ultima_actualizacion CHARACTER(40),
    CONSTRAINT PK_motivos_visita PRIMARY KEY (motivo_id)
);
