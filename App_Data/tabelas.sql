--create database dbBH;
use dbBH;
CREATE TABLE t01_entidade (
  t01_cd_entidade INT NOT NULL IDENTITY(1,1),
  nm_entidade VARCHAR(500) NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_deletado BIT NULL,
  PRIMARY KEY(t01_cd_entidade)
);


CREATE TABLE t02_usuario (
  t02_cd_usuario VARCHAR(20) NOT NULL,
  t01_cd_entidade INT NULL,
  nm_nome VARCHAR(200) NULL,
  nm_cargo VARCHAR(100) NULL,
  nm_email VARCHAR(100) NULL,
  nm_telefone VARCHAR(20) NULL,
  nm_celular VARCHAR(20) NULL,
  pw_senha NVARCHAR(50) NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  PRIMARY KEY(t02_cd_usuario)
);

CREATE TABLE t03_projeto (
  t03_cd_projeto INT NOT NULL IDENTITY(1,1),
  t02_cd_usuario VARCHAR(20) NOT NULL,
  t01_cd_entidade INT NOT NULL,
  t02_cd_usuario_monitoramento VARCHAR(20) NULL,
  nm_projeto VARCHAR(500) NULL,
  ds_publico TEXT NULL,
  ds_objetivo TEXT NULL,
  dt_inicio DATETIME NULL,
  dt_fim DATETIME NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  dt_atualizado DATETIME NULL,
  fl_deletado BIT NULL,
  t26_cd_arearesultado INT NULL,
  PRIMARY KEY(t03_cd_projeto)
);
--alter table t03_projeto add t26_cd_arearesultado INT NULL;
--update t03_projeto set t26_cd_arearesultado=1;

CREATE TABLE t04_parceiro (
  t04_cd_parceiro INT NOT NULL IDENTITY(1,1),
  t03_cd_projeto INT NULL,
  t01_cd_entidade INT NULL,
  nm_nome VARCHAR(200) NULL,
  ds_atuacao TEXT NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  PRIMARY KEY(t04_cd_parceiro)
);

CREATE TABLE t05_situacao (
  t05_cd_situacao INT NOT NULL IDENTITY(1,1),
  t03_cd_projeto INT NULL,
  ds_situacao TEXT NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  PRIMARY KEY(t05_cd_situacao)
);

CREATE TABLE t06_colaborador (
  t06_cd_colaborador INT NOT NULL IDENTITY(1,1),
  t03_cd_projeto INT NULL,
  t02_cd_usuario VARCHAR(20) NULL,
  nm_funcao VARCHAR(500) NULL,
  nu_ordem INT NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  PRIMARY KEY(t06_cd_colaborador)
);

CREATE TABLE t07_restricao (
  t07_cd_restricao INT NOT NULL IDENTITY(1,1),
  t03_cd_projeto INT NULL,
  ds_restricao TEXT NULL,
  ds_medida TEXT NULL,
  ds_providencia TEXT NULL,
  dt_superada DATETIME NULL,
  dt_limite DATETIME NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_deletado BIT NULL,
  PRIMARY KEY(t07_cd_restricao)
);

CREATE TABLE t08_acao (
  t08_cd_acao INT NOT NULL IDENTITY(1,1),
  t03_cd_projeto INT NULL,
  t02_cd_usuario VARCHAR(20) NULL,
  nm_acao VARCHAR(500) NULL,
  ds_acao TEXT NULL,
  dt_inicio DATETIME NULL,
  dt_fim DATETIME NULL,
  dt_original DATETIME NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_deletado BIT NULL,
  PRIMARY KEY(t08_cd_acao)
);

CREATE TABLE t09_marco (
  t09_cd_marco INT NOT NULL IDENTITY(1,1),
  t03_cd_projeto INT NULL,
  ds_marco TEXT NULL,
  nu_esforco INT NULL,
  dt_prevista DATETIME NULL,
  dt_realizada DATETIME NULL,
  dt_original DATETIME NULL,
  ds_comentario TEXT NULL,
  fl_status CHAR(1) NULL,
  nu_ordem INT NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_deletado BIT NULL,
  PRIMARY KEY(t09_cd_marco)
);

CREATE TABLE t10_produto (
  t10_cd_produto INT NOT NULL IDENTITY(1,1),
  t08_cd_acao INT NULL,
  ds_produto TEXT NULL,
  nm_medida VARCHAR(50) NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_deletado BIT NULL,
  PRIMARY KEY(t10_cd_produto)
);

CREATE TABLE t11_financeiro (
  t11_cd_financeiro INT NOT NULL IDENTITY(1,1),
  t27_cd_fonte INT NOT NULL,
  t08_cd_acao INT NOT NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_deletado BIT NULL,
  PRIMARY KEY(t11_cd_financeiro)
);

CREATE TABLE t12_resultado (
  t12_cd_resultado INT NOT NULL IDENTITY(1,1),
  t03_cd_projeto INT NULL,
  nm_resultado VARCHAR(500) NULL,
  ds_resultado TEXT NULL,
  nm_medida VARCHAR(200) NULL,
  nu_ano INT NULL,
  vl_t0 DECIMAL(18,2) NULL,
  fl_acumulado BIT NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_deletado BIT NULL,
  PRIMARY KEY(t12_cd_resultado)
);

CREATE TABLE t13_vlresultado (
  t13_cd_vlresultado INT NOT NULL IDENTITY(1,1),
  t12_cd_resultado INT NULL,
  nu_ano INT NULL,
  vl_previsto DECIMAL(18,2) NULL,
  vl_realizado DECIMAL(18,2) NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  PRIMARY KEY(t13_cd_vlresultado)
);

CREATE TABLE t14_documento (
  t14_cd_documento INT NOT NULL IDENTITY(1,1),
  t03_cd_projeto INT NULL,
  nm_documento VARCHAR(1000) NULL,
  ds_descricao TEXT NULL,
  nm_arquivo VARCHAR(500) NULL,
  fl_foto BIT NULL,
  fl_video BIT NULL,
  fl_cronograma BIT NULL,
  fl_outros BIT NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_deletado BIT NULL,
  PRIMARY KEY(t14_cd_documento)
);

CREATE TABLE t15_noticia (
  t15_cd_noticia INT NOT NULL IDENTITY(1,1),
  t03_cd_projeto INT NULL,
  nm_noticia VARCHAR(500) NULL,
  ds_noticia TEXT NULL,
  dt_data DATETIME NULL,
  nm_arquivo VARCHAR(500) NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_ativa BIT NULL,
  PRIMARY KEY(t15_cd_noticia)
);

CREATE TABLE t16_agenda (
  t16_cd_agenda INT NOT NULL IDENTITY(1,1),
  t03_cd_projeto INT NULL,
  nm_agenda VARCHAR(500) NULL,
  ds_agenda TEXT NULL,
  dt_data DATETIME NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_ativa BIT NULL,
  PRIMARY KEY(t16_cd_agenda)
);

CREATE TABLE t17_vlproduto (
  t17_cd_vlproduto INT NOT NULL IDENTITY(1,1),
  t10_cd_produto INT NULL,
  nu_ano INT NULL,
  vl_p1 DECIMAL(18,2) NULL,
  vl_p2 DECIMAL(18,2) NULL,
  vl_p3 DECIMAL(18,2) NULL,
  vl_p4 DECIMAL(18,2) NULL,
  vl_p5 DECIMAL(18,2) NULL,
  vl_p6 DECIMAL(18,2) NULL,
  vl_p7 DECIMAL(18,2) NULL,
  vl_p8 DECIMAL(18,2) NULL,
  vl_p9 DECIMAL(18,2) NULL,
  vl_p10 DECIMAL(18,2) NULL,
  vl_p11 DECIMAL(18,2) NULL,
  vl_p12 DECIMAL(18,2) NULL,
  vl_r1 DECIMAL(18,2) NULL,
  vl_r2 DECIMAL(18,2) NULL,
  vl_r3 DECIMAL(18,2) NULL,
  vl_r4 DECIMAL(18,2) NULL,
  vl_r5 DECIMAL(18,2) NULL,
  vl_r6 DECIMAL(18,2) NULL,
  vl_r7 DECIMAL(18,2) NULL,
  vl_r8 DECIMAL(18,2) NULL,
  vl_r9 DECIMAL(18,2) NULL,
  vl_r10 DECIMAL(18,2) NULL,
  vl_r11 DECIMAL(18,2) NULL,
  vl_r12 DECIMAL(18,2) NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  PRIMARY KEY(t17_cd_vlproduto)
);

CREATE TABLE t18_vlfinanceiro (
  t18_cd_vlfinanceiro INT NOT NULL IDENTITY(1,1),
  t11_cd_financeiro INT NULL,
  nu_ano INT NULL,
  vl_dotatual1 DECIMAL(18,2) NULL,
  vl_provisionado1 DECIMAL(18,2) NULL,
  vl_empenhado1 DECIMAL(18,2) NULL,
  vl_liquidado1 DECIMAL(18,2) NULL,
  vl_pago1 DECIMAL(18,2) NULL,
  vl_dotatual2 DECIMAL(18,2) NULL,
  vl_provisionado2 DECIMAL(18,2) NULL,
  vl_empenhado2 DECIMAL(18,2) NULL,
  vl_liquidado2 DECIMAL(18,2) NULL,
  vl_pago2 DECIMAL(18,2) NULL,
  vl_dotatual3 DECIMAL(18,2) NULL,
  vl_provisionado3 DECIMAL(18,2) NULL,
  vl_empenhado3 DECIMAL(18,2) NULL,
  vl_liquidado3 DECIMAL(18,2) NULL,
  vl_pago3 DECIMAL(18,2) NULL,
  vl_dotatual4 DECIMAL(18,2) NULL,
  vl_provisionado4 DECIMAL(18,2) NULL,
  vl_empenhado4 DECIMAL(18,2) NULL,
  vl_liquidado4 DECIMAL(18,2) NULL,
  vl_pago4 DECIMAL(18,2) NULL,
  vl_dotatual5 DECIMAL(18,2) NULL,
  vl_provisionado5 DECIMAL(18,2) NULL,
  vl_empenhado5 DECIMAL(18,2) NULL,
  vl_liquidado5 DECIMAL(18,2) NULL,
  vl_pago5 DECIMAL(18,2) NULL,
  vl_dotatual6 DECIMAL(18,2) NULL,
  vl_provisionado6 DECIMAL(18,2) NULL,
  vl_empenhado6 DECIMAL(18,2) NULL,
  vl_liquidado6 DECIMAL(18,2) NULL,
  vl_pago6 DECIMAL(18,2) NULL,
  vl_dotatual7 DECIMAL(18,2) NULL,
  vl_provisionado7 DECIMAL(18,2) NULL,
  vl_empenhado7 DECIMAL(18,2) NULL,
  vl_liquidado7 DECIMAL(18,2) NULL,
  vl_pago7 DECIMAL(18,2) NULL,
  vl_dotatual8 DECIMAL(18,2) NULL,
  vl_provisionado8 DECIMAL(18,2) NULL,
  vl_empenhado8 DECIMAL(18,2) NULL,
  vl_liquidado8 DECIMAL(18,2) NULL,
  vl_pago8 DECIMAL(18,2) NULL,
  vl_dotatual9 DECIMAL(18,2) NULL,
  vl_provisionado9 DECIMAL(18,2) NULL,
  vl_empenhado9 DECIMAL(18,2) NULL,
  vl_liquidado9 DECIMAL(18,2) NULL,
  vl_pago9 DECIMAL(18,2) NULL,
  vl_dotatual10 DECIMAL(18,2) NULL,
  vl_provisionado10 DECIMAL(18,2) NULL,
  vl_empenhado10 DECIMAL(18,2) NULL,
  vl_liquidado10 DECIMAL(18,2) NULL,
  vl_pago10 DECIMAL(18,2) NULL,
  vl_dotatual11 DECIMAL(18,2) NULL,
  vl_provisionado11 DECIMAL(18,2) NULL,
  vl_empenhado11 DECIMAL(18,2) NULL,
  vl_liquidado11 DECIMAL(18,2) NULL,
  vl_pago11 DECIMAL(18,2) NULL,
  vl_dotatual12 DECIMAL(18,2) NULL,
  vl_provisionado12 DECIMAL(18,2) NULL,
  vl_empenhado12 DECIMAL(18,2) NULL,
  vl_liquidado12 DECIMAL(18,2) NULL,
  vl_pago12 DECIMAL(18,2) NULL,
  vl_restopagar DECIMAL(18,2) NULL,
  vl_dotinicial DECIMAL(18,2) NULL,
  dt_cadastro DATETIME NULL,
  PRIMARY KEY(t18_cd_vlfinanceiro)
);



CREATE TABLE t19_marcorestricao (
  t19_cd_acaorestricao INT NOT NULL IDENTITY(1,1),
  t09_cd_marco INT NULL,
  t07_cd_restricao INT NULL,
  dt_cadastro DATETIME NULL,
  PRIMARY KEY(t19_cd_acaorestricao)
);

CREATE TABLE t20_acesso (
  t20_cd_acesso INT NOT NULL IDENTITY(1,1),
  t02_cd_usuario VARCHAR(20) NULL,
  nm_ip VARCHAR(100) NULL,
  dt_data DATETIME NULL,
  PRIMARY KEY(t20_cd_acesso)
);

CREATE TABLE t21_fase (
  t21_cd_fase INT NOT NULL IDENTITY(1,1),
  nm_fase VARCHAR(200) NULL,
  ds_fase TEXT NULL,
  fl_fase CHAR(2) NULL,
  nm_arquivo VARCHAR(200) NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  PRIMARY KEY(t21_cd_fase)
);
insert into t21_fase (nm_fase, fl_fase) values('Em Estruturação','ES');
insert into t21_fase (nm_fase, fl_fase) values('Em Execução','EX');
insert into t21_fase (nm_fase, fl_fase) values('Em Revisão','RE');
insert into t21_fase (nm_fase, fl_fase) values('Concluído','CO');
insert into t21_fase (nm_fase, fl_fase) values('Encerrado','EN');


CREATE TABLE t22_faseprojeto (
  t22_cd_faseprojeto INT NOT NULL IDENTITY(1,1),
  t21_cd_fase INT  NULL,
  t03_cd_projeto INT  NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  PRIMARY KEY(t22_cd_faseprojeto)
);

CREATE TABLE t23_providencia (
  t23_cd_providencia INT NOT NULL IDENTITY(1,1),
  t07_cd_restricao INT NULL,
  t02_cd_usuario VARCHAR(20) NULL,
  ds_providencia TEXT NULL,
  fl_gerente BIT NULL,
  dt_cadastro DATETIME NULL,
  PRIMARY KEY(t23_cd_providencia)
);

CREATE TABLE t24_perfil (
  t24_cd_perfil INT NOT NULL IDENTITY(1,1),
  nm_perfil VARCHAR(500) NULL,
  ds_perfil TEXT NULL,
  fl_perfil VARCHAR(100) NULL,
  fl_ativa BIT NULL,
  nu_ordem INT NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  PRIMARY KEY(t24_cd_perfil)
);

INSERT INTO t24_perfil 
(nm_perfil, ds_perfil, fl_perfil, fl_ativa, nu_ordem, dt_cadastro, dt_alterado) 
VALUES('Administrador Geral','Administra os usuários, entidades e projetos', 'fl_admin', 1, 1, getdate(),getdate());

INSERT INTO t24_perfil 
(nm_perfil, ds_perfil, fl_perfil, fl_ativa, nu_ordem, dt_cadastro, dt_alterado) 
VALUES('Monitoramento','Acesso a área de monitoramento dos projetos', 'fl_monitora', 1, 2, getdate(),getdate());

INSERT INTO t24_perfil 
(nm_perfil, ds_perfil, fl_perfil, fl_ativa, nu_ordem, dt_cadastro, dt_alterado) 
VALUES('Estratégico','Acesso restrito a todos os projetos', 'fl_estrategico', 1, 3, getdate(),getdate());


CREATE TABLE t25_usuarioperfil (
  t25_cd_usuarioperfil INT NOT NULL IDENTITY(1,1),
  t02_cd_usuario VARCHAR(20) NOT NULL,
  t24_cd_perfil INT NOT NULL,
  dt_cadastro DATETIME NULL,
  PRIMARY KEY(t25_cd_usuarioperfil)
);


CREATE TABLE t26_arearesultado (
  t26_cd_arearesultado INT NOT NULL IDENTITY(1,1),
  nm_area VARCHAR(500) NULL,
  ds_area TEXT NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_deletado BIT NULL,
  PRIMARY KEY(t26_cd_arearesultado)
);

CREATE TABLE t27_fonte (
  t27_cd_fonte INT NOT NULL IDENTITY(1,1),
  nm_fonte VARCHAR(500) NULL,
  dt_cadastro DATETIME NULL,
  dt_alterado DATETIME NULL,
  fl_deletado BIT NULL,
  PRIMARY KEY(t27_cd_fonte)
);

