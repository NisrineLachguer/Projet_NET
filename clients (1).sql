-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le : sam. 04 jan. 2025 à 00:56
-- Version du serveur : 8.3.0
-- Version de PHP : 8.2.18

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `hotel_management_wpf`
--

-- --------------------------------------------------------

--
-- Structure de la table `clients`
--

DROP TABLE IF EXISTS `clients`;
CREATE TABLE IF NOT EXISTS `clients` (
  `ClientID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `Email` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `PhoneNumber` varchar(15) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Address` text COLLATE utf8mb4_general_ci,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ClientID`)
) ENGINE=MyISAM AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `clients`
--

INSERT INTO `clients` (`ClientID`, `Name`, `Email`, `PhoneNumber`, `Address`, `created_at`) VALUES
(11, 'irharisa', 'Imane ', '0257845', 'hhdjkhdz', '2024-01-08 23:48:37'),
(7, 'nisrine lachguer', 'nisrine@gmail.com', '06060666', 'Marrakech SYBA 145', '2025-01-03 23:48:37'),
(12, 'Loubna', 'irharisa@gmail.com', '0606063254', 'Lhbichat Ourika', '2025-01-14 23:48:37'),
(13, 'Alami', 'lachguer@gmail.com', '06060600', 'SYBA marrakech', '2025-01-03 23:48:37'),
(16, 'Fati Ezzahra', 'Fati@gmail.com', '74185208563', 'El fadle Marrakech', '2025-01-03 23:48:37'),
(17, 'Zouhir', 'zouhir@gmail.com', '062541789', 'Targa Marrakech', '2025-01-03 23:48:37');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
