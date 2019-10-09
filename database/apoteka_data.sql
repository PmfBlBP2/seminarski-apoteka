-- MySQL dump 10.13  Distrib 5.7.25, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: apoteka
-- ------------------------------------------------------
-- Server version	5.5.5-10.1.36-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Dumping data for table `dobavljac`
--

LOCK TABLES `dobavljac` WRITE;
/*!40000 ALTER TABLE `dobavljac` DISABLE KEYS */;
INSERT INTO `dobavljac` VALUES (1,1,'Hemofarm','Novakovici bb','051/456-758',NULL),(2,4,'Farmalogist','Mirijevski bulevar 3','+381113315090',NULL),(3,5,'Alkaloid','Bul.A.Makedonski 12','+38220621776',NULL),(4,4,'Galenika','Batajnicki drum bb','+381113315090',NULL);
/*!40000 ALTER TABLE `dobavljac` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `drzava`
--

LOCK TABLES `drzava` WRITE;
/*!40000 ALTER TABLE `drzava` DISABLE KEYS */;
INSERT INTO `drzava` VALUES (1,'BiH'),(2,'Srbija'),(3,'Makedonija'),(4,'Hrvatska'),(5,'Slovenija');
/*!40000 ALTER TABLE `drzava` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `grad`
--

LOCK TABLES `grad` WRITE;
/*!40000 ALTER TABLE `grad` DISABLE KEYS */;
INSERT INTO `grad` VALUES (1,1,'Banja Luka','78000'),(2,1,'Sarajevo','71000'),(3,1,'Mostar',NULL),(4,2,'Beograd',NULL),(5,3,'Skoplje',NULL);
/*!40000 ALTER TABLE `grad` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `kupovina`
--

LOCK TABLES `kupovina` WRITE;
/*!40000 ALTER TABLE `kupovina` DISABLE KEYS */;
/*!40000 ALTER TABLE `kupovina` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `lijek`
--

LOCK TABLES `lijek` WRITE;
/*!40000 ALTER TABLE `lijek` DISABLE KEYS */;
INSERT INTO `lijek` VALUES (1,1,'Otol H',1,10,50),(2,3,'Pholcodin',1,18,30),(3,4,'Chloramphenicol',1,4,50),(4,4,'Pantenol',0,5,50),(5,2,'Probiotik',0,16,30),(6,1,'Ibuprofen',1,6,50),(7,3,'Caffetin',0,3,50);
/*!40000 ALTER TABLE `lijek` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `osiguranik`
--

LOCK TABLES `osiguranik` WRITE;
/*!40000 ALTER TABLE `osiguranik` DISABLE KEYS */;
INSERT INTO `osiguranik` VALUES (1,'2705965105436','Srdjan','Jovic',1,'Ive Andrica 56',NULL),(2,'1209987105236','Marija','Petric',1,'Krfska 65',NULL),(3,'3006995105232','Jovan','Lucic',2,'Gavrila Principa 12',NULL),(4,'0904978105462','Jelica','Stevic',1,'Cara Lazara 13',NULL),(5,'1103998105478','Nikola','Markovic',1,'Carice Milice 12',NULL);
/*!40000 ALTER TABLE `osiguranik` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `racun`
--

LOCK TABLES `racun` WRITE;
/*!40000 ALTER TABLE `racun` DISABLE KEYS */;
/*!40000 ALTER TABLE `racun` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-10-09 13:34:55
