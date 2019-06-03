CREATE DATABASE  IF NOT EXISTS `Faculdade` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `Faculdade`;
-- MySQL dump 10.13  Distrib 8.0.16, for Win64 (x86_64)
--
-- Host: localhost    Database: Faculdade
-- ------------------------------------------------------
-- Server version	5.7.26-0ubuntu0.18.04.1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Alunos`
--

DROP TABLE IF EXISTS `Alunos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Alunos` (
  `alunoId` int(11) NOT NULL AUTO_INCREMENT,
  `alunoFkPessoa` int(11) DEFAULT NULL,
  `alunoStatus` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`alunoId`),
  KEY `alunoFkPessoa` (`alunoFkPessoa`),
  CONSTRAINT `Alunos_ibfk_1` FOREIGN KEY (`alunoFkPessoa`) REFERENCES `Pessoas` (`pessoaId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Alunos`
--

LOCK TABLES `Alunos` WRITE;
/*!40000 ALTER TABLE `Alunos` DISABLE KEYS */;
/*!40000 ALTER TABLE `Alunos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Cargos`
--

DROP TABLE IF EXISTS `Cargos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Cargos` (
  `cargoId` int(11) NOT NULL AUTO_INCREMENT,
  `cargoNome` varchar(255) DEFAULT NULL,
  `cargoSalario` float DEFAULT NULL,
  `cargoStatus` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`cargoId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Cargos`
--

LOCK TABLES `Cargos` WRITE;
/*!40000 ALTER TABLE `Cargos` DISABLE KEYS */;
/*!40000 ALTER TABLE `Cargos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Contas`
--

DROP TABLE IF EXISTS `Contas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Contas` (
  `contaId` int(11) NOT NULL AUTO_INCREMENT,
  `contaNome` varchar(255) DEFAULT NULL,
  `contaSaldo` float DEFAULT NULL,
  `contaStatus` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`contaId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Contas`
--

LOCK TABLES `Contas` WRITE;
/*!40000 ALTER TABLE `Contas` DISABLE KEYS */;
/*!40000 ALTER TABLE `Contas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ContratoServicos`
--

DROP TABLE IF EXISTS `ContratoServicos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ContratoServicos` (
  `contratoServicoId` int(11) NOT NULL AUTO_INCREMENT,
  `contratoServicoFkContrato` int(11) DEFAULT NULL,
  `contratoServicosFkServico` int(11) DEFAULT NULL,
  PRIMARY KEY (`contratoServicoId`),
  KEY `contratoServicoFkContrato` (`contratoServicoFkContrato`),
  KEY `contratoServicosFkServico` (`contratoServicosFkServico`),
  CONSTRAINT `ContratoServicos_ibfk_1` FOREIGN KEY (`contratoServicoFkContrato`) REFERENCES `Contratos` (`contratoId`),
  CONSTRAINT `ContratoServicos_ibfk_2` FOREIGN KEY (`contratoServicosFkServico`) REFERENCES `Servicos` (`servicoId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ContratoServicos`
--

LOCK TABLES `ContratoServicos` WRITE;
/*!40000 ALTER TABLE `ContratoServicos` DISABLE KEYS */;
/*!40000 ALTER TABLE `ContratoServicos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Contratos`
--

DROP TABLE IF EXISTS `Contratos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Contratos` (
  `contratoId` int(11) NOT NULL AUTO_INCREMENT,
  `contratoFkAluno` int(11) DEFAULT NULL,
  `contratoData` datetime DEFAULT NULL,
  `contratoVencimento` datetime DEFAULT NULL,
  `contratoAtivo` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`contratoId`),
  KEY `contratoFkAluno` (`contratoFkAluno`),
  CONSTRAINT `Contratos_ibfk_1` FOREIGN KEY (`contratoFkAluno`) REFERENCES `Alunos` (`alunoId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Contratos`
--

LOCK TABLES `Contratos` WRITE;
/*!40000 ALTER TABLE `Contratos` DISABLE KEYS */;
/*!40000 ALTER TABLE `Contratos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `DataLogs`
--

DROP TABLE IF EXISTS `DataLogs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `DataLogs` (
  `dataLogId` int(11) NOT NULL AUTO_INCREMENT,
  `dataLogData` datetime DEFAULT NULL,
  `dataLogFkUsuario` int(11) DEFAULT NULL,
  `dataLogAtividade` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`dataLogId`),
  KEY `dataLogFkUsuario` (`dataLogFkUsuario`),
  CONSTRAINT `DataLogs_ibfk_1` FOREIGN KEY (`dataLogFkUsuario`) REFERENCES `Usuarios` (`usuarioId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `DataLogs`
--

LOCK TABLES `DataLogs` WRITE;
/*!40000 ALTER TABLE `DataLogs` DISABLE KEYS */;
/*!40000 ALTER TABLE `DataLogs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Documentos`
--

DROP TABLE IF EXISTS `Documentos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Documentos` (
  `documentoId` int(11) NOT NULL AUTO_INCREMENT,
  `documentoFkTipo` int(11) DEFAULT NULL,
  `documentoNumero` mediumtext,
  `documentoStatus` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`documentoId`),
  KEY `documentoFkTipo` (`documentoFkTipo`),
  CONSTRAINT `Documentos_ibfk_1` FOREIGN KEY (`documentoFkTipo`) REFERENCES `TiposDocumento` (`tipoDocumentoId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Documentos`
--

LOCK TABLES `Documentos` WRITE;
/*!40000 ALTER TABLE `Documentos` DISABLE KEYS */;
/*!40000 ALTER TABLE `Documentos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Enderecos`
--

DROP TABLE IF EXISTS `Enderecos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Enderecos` (
  `enderecoId` int(11) NOT NULL AUTO_INCREMENT,
  `enderecoLogradouro` varchar(10) DEFAULT NULL,
  `enderecoDesc` varchar(255) DEFAULT NULL,
  `enderecoNumero` int(11) DEFAULT NULL,
  `enderecoBairro` varchar(10) DEFAULT NULL,
  `enderecoCidade` varchar(255) DEFAULT NULL,
  `enderecoCep` int(8) DEFAULT NULL,
  `enderecoStatus` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`enderecoId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Enderecos`
--

LOCK TABLES `Enderecos` WRITE;
/*!40000 ALTER TABLE `Enderecos` DISABLE KEYS */;
/*!40000 ALTER TABLE `Enderecos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `FuncionarioServico`
--

DROP TABLE IF EXISTS `FuncionarioServico`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `FuncionarioServico` (
  `fkFuncionario` int(11) DEFAULT NULL,
  `fkServico` int(11) DEFAULT NULL,
  KEY `fkFuncionario` (`fkFuncionario`),
  KEY `fkServico` (`fkServico`),
  CONSTRAINT `FuncionarioServico_ibfk_1` FOREIGN KEY (`fkFuncionario`) REFERENCES `Funcionarios` (`funcionarioId`),
  CONSTRAINT `FuncionarioServico_ibfk_2` FOREIGN KEY (`fkServico`) REFERENCES `Servicos` (`servicoId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `FuncionarioServico`
--

LOCK TABLES `FuncionarioServico` WRITE;
/*!40000 ALTER TABLE `FuncionarioServico` DISABLE KEYS */;
/*!40000 ALTER TABLE `FuncionarioServico` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Funcionarios`
--

DROP TABLE IF EXISTS `Funcionarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Funcionarios` (
  `funcionarioId` int(11) NOT NULL AUTO_INCREMENT,
  `funcionarioFkPessoa` int(11) DEFAULT NULL,
  `funcionarioStatus` tinyint(1) DEFAULT NULL,
  `funcionarioCargo` int(11) DEFAULT NULL,
  PRIMARY KEY (`funcionarioId`),
  KEY `funcionarioFkPessoa` (`funcionarioFkPessoa`),
  KEY `funcionarioCargo` (`funcionarioCargo`),
  CONSTRAINT `Funcionarios_ibfk_1` FOREIGN KEY (`funcionarioFkPessoa`) REFERENCES `Pessoas` (`pessoaId`),
  CONSTRAINT `Funcionarios_ibfk_2` FOREIGN KEY (`funcionarioCargo`) REFERENCES `Cargos` (`cargoId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Funcionarios`
--

LOCK TABLES `Funcionarios` WRITE;
/*!40000 ALTER TABLE `Funcionarios` DISABLE KEYS */;
/*!40000 ALTER TABLE `Funcionarios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Movimentacoes`
--

DROP TABLE IF EXISTS `Movimentacoes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Movimentacoes` (
  `movimentoId` int(11) NOT NULL AUTO_INCREMENT,
  `movimentoTipo` char(3) DEFAULT NULL,
  `movimentOrigem` varchar(255) DEFAULT NULL,
  `movimentoValor` float DEFAULT NULL,
  `movimentoFkConta` int(11) DEFAULT NULL,
  `movimentoFkUsuario` int(11) DEFAULT NULL,
  `movimentoFkPessoa` int(11) DEFAULT NULL,
  `movimentoDataEmissao` datetime DEFAULT NULL,
  `movimentoDataPagamento` datetime DEFAULT NULL,
  PRIMARY KEY (`movimentoId`),
  KEY `movimentoFkConta` (`movimentoFkConta`),
  KEY `movimentoFkUsuario` (`movimentoFkUsuario`),
  KEY `movimentoFkPessoa` (`movimentoFkPessoa`),
  CONSTRAINT `Movimentacoes_ibfk_1` FOREIGN KEY (`movimentoFkConta`) REFERENCES `Contas` (`contaId`),
  CONSTRAINT `Movimentacoes_ibfk_2` FOREIGN KEY (`movimentoFkUsuario`) REFERENCES `Usuarios` (`usuarioId`),
  CONSTRAINT `Movimentacoes_ibfk_3` FOREIGN KEY (`movimentoFkPessoa`) REFERENCES `Pessoas` (`pessoaId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Movimentacoes`
--

LOCK TABLES `Movimentacoes` WRITE;
/*!40000 ALTER TABLE `Movimentacoes` DISABLE KEYS */;
/*!40000 ALTER TABLE `Movimentacoes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `NivelAcesso`
--

DROP TABLE IF EXISTS `NivelAcesso`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `NivelAcesso` (
  `nivelAcessoId` int(11) NOT NULL AUTO_INCREMENT,
  `nivelAcessoNome` varchar(20) DEFAULT NULL,
  `nivelAcessoStatus` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`nivelAcessoId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `NivelAcesso`
--

LOCK TABLES `NivelAcesso` WRITE;
/*!40000 ALTER TABLE `NivelAcesso` DISABLE KEYS */;
INSERT INTO `NivelAcesso` VALUES (1,'SuperUser',1);
/*!40000 ALTER TABLE `NivelAcesso` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Pessoas`
--

DROP TABLE IF EXISTS `Pessoas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Pessoas` (
  `pessoaId` int(11) NOT NULL AUTO_INCREMENT,
  `pessoaNome` varchar(30) DEFAULT NULL,
  `pessoaSobreNome` varchar(30) DEFAULT NULL,
  `pessoaJuridica` tinyint(1) DEFAULT NULL,
  `pessoaStatus` tinyint(1) DEFAULT NULL,
  `pessoaSexo` tinyint(1) DEFAULT NULL,
  `pessoaNascimento` date DEFAULT NULL,
  PRIMARY KEY (`pessoaId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Pessoas`
--

LOCK TABLES `Pessoas` WRITE;
/*!40000 ALTER TABLE `Pessoas` DISABLE KEYS */;
INSERT INTO `Pessoas` VALUES (1,'Administrador','',0,1,1,'2000-05-23');
/*!40000 ALTER TABLE `Pessoas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PessoasDocumentos`
--

DROP TABLE IF EXISTS `PessoasDocumentos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `PessoasDocumentos` (
  `fkPessoa` int(11) DEFAULT NULL,
  `fkDocumento` int(11) DEFAULT NULL,
  KEY `fkPessoa` (`fkPessoa`),
  KEY `fkDocumento` (`fkDocumento`),
  CONSTRAINT `PessoasDocumentos_ibfk_1` FOREIGN KEY (`fkPessoa`) REFERENCES `Pessoas` (`pessoaId`),
  CONSTRAINT `PessoasDocumentos_ibfk_2` FOREIGN KEY (`fkDocumento`) REFERENCES `Documentos` (`documentoId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PessoasDocumentos`
--

LOCK TABLES `PessoasDocumentos` WRITE;
/*!40000 ALTER TABLE `PessoasDocumentos` DISABLE KEYS */;
/*!40000 ALTER TABLE `PessoasDocumentos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PessoasEnderecos`
--

DROP TABLE IF EXISTS `PessoasEnderecos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `PessoasEnderecos` (
  `fkPessoa` int(11) DEFAULT NULL,
  `fkEndereco` int(11) DEFAULT NULL,
  KEY `fkPessoa` (`fkPessoa`),
  KEY `fkEndereco` (`fkEndereco`),
  CONSTRAINT `PessoasEnderecos_ibfk_1` FOREIGN KEY (`fkPessoa`) REFERENCES `Pessoas` (`pessoaId`),
  CONSTRAINT `PessoasEnderecos_ibfk_2` FOREIGN KEY (`fkEndereco`) REFERENCES `Enderecos` (`enderecoId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PessoasEnderecos`
--

LOCK TABLES `PessoasEnderecos` WRITE;
/*!40000 ALTER TABLE `PessoasEnderecos` DISABLE KEYS */;
/*!40000 ALTER TABLE `PessoasEnderecos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PessoasTelefone`
--

DROP TABLE IF EXISTS `PessoasTelefone`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `PessoasTelefone` (
  `fkPessoa` int(11) DEFAULT NULL,
  `fkTelefone` int(11) DEFAULT NULL,
  KEY `fkPessoa` (`fkPessoa`),
  KEY `fkTelefone` (`fkTelefone`),
  CONSTRAINT `PessoasTelefone_ibfk_1` FOREIGN KEY (`fkPessoa`) REFERENCES `Pessoas` (`pessoaId`),
  CONSTRAINT `PessoasTelefone_ibfk_2` FOREIGN KEY (`fkTelefone`) REFERENCES `Telefones` (`telefoneId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PessoasTelefone`
--

LOCK TABLES `PessoasTelefone` WRITE;
/*!40000 ALTER TABLE `PessoasTelefone` DISABLE KEYS */;
/*!40000 ALTER TABLE `PessoasTelefone` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Servicos`
--

DROP TABLE IF EXISTS `Servicos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Servicos` (
  `servicoId` int(11) NOT NULL AUTO_INCREMENT,
  `servicoNome` varchar(40) DEFAULT NULL,
  `servicoStaus` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`servicoId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Servicos`
--

LOCK TABLES `Servicos` WRITE;
/*!40000 ALTER TABLE `Servicos` DISABLE KEYS */;
/*!40000 ALTER TABLE `Servicos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Telefones`
--

DROP TABLE IF EXISTS `Telefones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Telefones` (
  `telefoneId` int(11) NOT NULL AUTO_INCREMENT,
  `telefoneDdd` int(2) DEFAULT NULL,
  `telefoneNumero` int(11) DEFAULT NULL,
  `telefoneFkTipo` int(11) DEFAULT NULL,
  PRIMARY KEY (`telefoneId`),
  KEY `telefoneFkTipo` (`telefoneFkTipo`),
  CONSTRAINT `Telefones_ibfk_1` FOREIGN KEY (`telefoneFkTipo`) REFERENCES `TiposTelefone` (`tipoTelefoneId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Telefones`
--

LOCK TABLES `Telefones` WRITE;
/*!40000 ALTER TABLE `Telefones` DISABLE KEYS */;
/*!40000 ALTER TABLE `Telefones` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `TiposDocumento`
--

DROP TABLE IF EXISTS `TiposDocumento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `TiposDocumento` (
  `tipoDocumentoId` int(11) NOT NULL AUTO_INCREMENT,
  `tipoDocumentoNome` varchar(20) DEFAULT NULL,
  `tipoDocumentoStatus` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`tipoDocumentoId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TiposDocumento`
--

LOCK TABLES `TiposDocumento` WRITE;
/*!40000 ALTER TABLE `TiposDocumento` DISABLE KEYS */;
/*!40000 ALTER TABLE `TiposDocumento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `TiposTelefone`
--

DROP TABLE IF EXISTS `TiposTelefone`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `TiposTelefone` (
  `tipoTelefoneId` int(11) NOT NULL AUTO_INCREMENT,
  `tipoTelefoneNome` varchar(255) DEFAULT NULL,
  `tipoTelefoneStatus` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`tipoTelefoneId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TiposTelefone`
--

LOCK TABLES `TiposTelefone` WRITE;
/*!40000 ALTER TABLE `TiposTelefone` DISABLE KEYS */;
/*!40000 ALTER TABLE `TiposTelefone` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Usuarios`
--

DROP TABLE IF EXISTS `Usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Usuarios` (
  `usuarioId` int(11) NOT NULL AUTO_INCREMENT,
  `usuarioLogin` varchar(255) DEFAULT NULL,
  `usuarioSenha` varchar(255) DEFAULT NULL,
  `usuarioFkNivelAcesso` int(11) DEFAULT NULL,
  `usuarioFkPessoa` int(11) DEFAULT NULL,
  `usuarioStatus` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`usuarioId`),
  KEY `usuarioFkNivelAcesso` (`usuarioFkNivelAcesso`),
  KEY `usuarioFkPessoa` (`usuarioFkPessoa`),
  CONSTRAINT `Usuarios_ibfk_1` FOREIGN KEY (`usuarioFkNivelAcesso`) REFERENCES `NivelAcesso` (`nivelAcessoId`),
  CONSTRAINT `Usuarios_ibfk_2` FOREIGN KEY (`usuarioFkPessoa`) REFERENCES `Pessoas` (`pessoaId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Usuarios`
--

LOCK TABLES `Usuarios` WRITE;
/*!40000 ALTER TABLE `Usuarios` DISABLE KEYS */;
INSERT INTO `Usuarios` VALUES (1,'admin','26d6a8ad97c75ffc548f6873e5e93ce475479e3e1a1097381e54221fb53ec1d2',1,1,1);
/*!40000 ALTER TABLE `Usuarios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `precoServico`
--

DROP TABLE IF EXISTS `precoServico`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `precoServico` (
  `precoServicoId` int(11) NOT NULL AUTO_INCREMENT,
  `precoServicoValor` float DEFAULT NULL,
  `precoServicoDataInicial` datetime DEFAULT NULL,
  `precoServicoDataFinal` datetime DEFAULT NULL,
  `precoServicoFkServico` int(11) DEFAULT NULL,
  PRIMARY KEY (`precoServicoId`),
  KEY `precoServicoFkServico` (`precoServicoFkServico`),
  CONSTRAINT `precoServico_ibfk_1` FOREIGN KEY (`precoServicoFkServico`) REFERENCES `Servicos` (`servicoId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `precoServico`
--

LOCK TABLES `precoServico` WRITE;
/*!40000 ALTER TABLE `precoServico` DISABLE KEYS */;
/*!40000 ALTER TABLE `precoServico` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-06-03 10:04:39
