-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema apoteka
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema apoteka
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `apoteka` DEFAULT CHARACTER SET latin1 ;
USE `apoteka` ;

-- -----------------------------------------------------
-- Table `apoteka`.`drzava`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `apoteka`.`drzava` ;

CREATE TABLE IF NOT EXISTS `apoteka`.`drzava` (
  `DrzavaId` INT(11) NOT NULL AUTO_INCREMENT,
  `Naziv` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`DrzavaId`))
ENGINE = InnoDB
AUTO_INCREMENT = 6
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `apoteka`.`grad`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `apoteka`.`grad` ;

CREATE TABLE IF NOT EXISTS `apoteka`.`grad` (
  `GradId` INT(11) NOT NULL AUTO_INCREMENT,
  `DrzavaId` INT(11) NOT NULL,
  `Naziv` VARCHAR(45) NULL DEFAULT NULL,
  `PostanskiBroj` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`GradId`),
  INDEX `fk_grad_drzava1_idx` (`DrzavaId` ASC),
  CONSTRAINT `fk_grad_drzava1`
    FOREIGN KEY (`DrzavaId`)
    REFERENCES `apoteka`.`drzava` (`DrzavaId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 6
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `apoteka`.`dobavljac`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `apoteka`.`dobavljac` ;

CREATE TABLE IF NOT EXISTS `apoteka`.`dobavljac` (
  `DobavljacId` INT(11) NOT NULL AUTO_INCREMENT,
  `GradId` INT(11) NOT NULL,
  `Naziv` VARCHAR(45) NULL DEFAULT NULL,
  `Adresa` VARCHAR(45) NULL DEFAULT NULL,
  `Telefon` VARCHAR(45) NULL DEFAULT NULL,
  `E-mail` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`DobavljacId`),
  INDEX `fk_dobavljac_grad1_idx` (`GradId` ASC),
  CONSTRAINT `fk_dobavljac_grad1`
    FOREIGN KEY (`GradId`)
    REFERENCES `apoteka`.`grad` (`GradId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 5
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `apoteka`.`lijek`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `apoteka`.`lijek` ;

CREATE TABLE IF NOT EXISTS `apoteka`.`lijek` (
  `LijekId` INT(11) NOT NULL AUTO_INCREMENT,
  `DobavljacId` INT(11) NOT NULL,
  `Naziv` VARCHAR(45) NULL DEFAULT NULL,
  `NaRecept` TINYINT(4) NULL DEFAULT NULL,
  `Cijena` DECIMAL(10,2) NULL DEFAULT NULL,
  `Kolicina` INT(11) NULL DEFAULT NULL,
  PRIMARY KEY (`LijekId`),
  INDEX `fk_lijek_dobavljac1_idx` (`DobavljacId` ASC),
  CONSTRAINT `fk_lijek_dobavljac1`
    FOREIGN KEY (`DobavljacId`)
    REFERENCES `apoteka`.`dobavljac` (`DobavljacId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 8
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `apoteka`.`racun`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `apoteka`.`racun` ;

CREATE TABLE IF NOT EXISTS `apoteka`.`racun` (
  `RacunId` INT(11) NOT NULL AUTO_INCREMENT,
  `Iznos` DECIMAL(10,2) NULL DEFAULT NULL,
  `DatumIzdavanja` DATE NULL DEFAULT NULL,
  PRIMARY KEY (`RacunId`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `apoteka`.`kupovina`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `apoteka`.`kupovina` ;

CREATE TABLE IF NOT EXISTS `apoteka`.`kupovina` (
  `RacunId` INT(11) NOT NULL,
  `LijekId` INT(11) NOT NULL,
  `Kolicina` INT(11) NULL DEFAULT NULL,
  `Iznos` DECIMAL(10,2) NULL DEFAULT NULL,
  INDEX `fk_kupovina_racun1_idx` (`RacunId` ASC),
  INDEX `fk_kupovina_lijek1_idx` (`LijekId` ASC),
  PRIMARY KEY (`RacunId`, `LijekId`),
  CONSTRAINT `fk_kupovina_lijek1`
    FOREIGN KEY (`LijekId`)
    REFERENCES `apoteka`.`lijek` (`LijekId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_kupovina_racun1`
    FOREIGN KEY (`RacunId`)
    REFERENCES `apoteka`.`racun` (`RacunId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `apoteka`.`osiguranik`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `apoteka`.`osiguranik` ;

CREATE TABLE IF NOT EXISTS `apoteka`.`osiguranik` (
  `OsiguranikId` INT(11) NOT NULL AUTO_INCREMENT,
  `JMBG` VARCHAR(45) NULL DEFAULT NULL,
  `Ime` VARCHAR(45) NULL DEFAULT NULL,
  `Prezime` VARCHAR(45) NULL DEFAULT NULL,
  `GradId` INT(11) NOT NULL,
  `Adresa` VARCHAR(45) NULL DEFAULT NULL,
  `Broj telefona` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`OsiguranikId`),
  INDEX `fk_osiguranik_grad_idx` (`GradId` ASC),
  CONSTRAINT `fk_osiguranik_grad`
    FOREIGN KEY (`GradId`)
    REFERENCES `apoteka`.`grad` (`GradId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 6
DEFAULT CHARACTER SET = latin1;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
