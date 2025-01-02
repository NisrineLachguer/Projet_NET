-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le : jeu. 02 jan. 2025 à 09:22
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
-- Structure de la table `employees`
--

DROP TABLE IF EXISTS `employees`;
CREATE TABLE IF NOT EXISTS `employees` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `role` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `email` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `phone` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `address` text COLLATE utf8mb4_general_ci,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `PhotoPath` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `employees`
--

INSERT INTO `employees` (`id`, `name`, `role`, `email`, `phone`, `address`, `created_at`, `PhotoPath`) VALUES
(13, 'sara', 'Front Desk Manager', 'dddddx', 'qqqq', 'qqqqq', '2025-01-02 07:44:43', 'C:\\Users\\irharissa.NPIF\\Desktop\\EMSI\\.NET\\Projet_NET\\hotel_management_wpf\\Gestion_hotel\\Images\\profileEmploye\\e1532876-71cc-4b4e-97a8-da42a2540337.jpeg'),
(15, 'mohamed', 'Hotel Manager', 'mohamed@gmail.com', '06254177455', 'tasoultant marrakech', '2025-01-02 08:27:26', 'C:\\Users\\irharissa.NPIF\\Desktop\\EMSI\\.NET\\Projet_NET\\hotel_management_wpf\\Gestion_hotel\\Images\\profileEmploye\\17a1361f-7bda-4bff-8e9a-d59b187e515e.jpeg'),
(16, 'mariem', 'Housekeeper', 'mary@gmail.com', '0258741365', 'exemple', '2025-01-02 08:29:12', 'C:\\Users\\irharissa.NPIF\\Desktop\\EMSI\\.NET\\Projet_NET\\hotel_management_wpf\\Gestion_hotel\\Images\\profileEmploye\\5f32aa29-11c7-4237-8c6c-6f926a8de5a9.jpeg'),
(17, 'Ayoube', 'Housekeeping Manager', 'ayoube@gmail.com', '025874123654', 'Massira 3 Marrakech', '2025-01-02 08:57:03', 'C:\\Users\\irharissa.NPIF\\Desktop\\EMSI\\.NET\\Projet_NET\\hotel_management_wpf\\Gestion_hotel\\Images\\profileEmploye\\439ee21d-ab33-4810-9091-2f99a89deb42.jpeg');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
