-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2026. Már 24. 11:15
-- Kiszolgáló verziója: 9.9.0
-- PHP verzió: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `eurotrip`
--
CREATE DATABASE IF NOT EXISTS `eurotrip` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `eurotrip`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `accommodation`
--

CREATE TABLE `accommodation` (
  `accommodation_id` int(11) NOT NULL,
  `name` varchar(100) NOT NULL,
  `address` varchar(150) NOT NULL,
  `image` varchar(100) DEFAULT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `city_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- A tábla adatainak kiíratása `accommodation`
--

INSERT INTO `accommodation` (`accommodation_id`, `name`, `address`, `image`, `phone`, `city_id`) VALUES
(1, 'Gellért Szálló', 'Szent Gellért tér 1.', NULL, NULL, 1),
(2, 'Berlin Központ Hotel', 'Alexanderplatz 1.', NULL, NULL, 2),
(3, 'Louvre Panzió', 'Rue de Rivoli 20.', NULL, NULL, 3),
(4, 'Római Vakáció Hotel', 'Piazza Venezia 5.', NULL, NULL, 4),
(5, 'Madrid Plaza Szálló', 'Plaza Mayor 10.', NULL, NULL, 5),
(6, 'Schönbrunn Apartman', 'Schloßstraße 1.', NULL, NULL, 6),
(7, 'Varsó Óváros Hotel', 'Rynek Starego Miasta 2.', NULL, NULL, 7),
(8, 'Károly Híd Panzió', 'Karlova 5.', NULL, NULL, 8),
(9, 'Duna-parti Szálló Pozsony', 'Rázusovo nábrežie 1.', NULL, NULL, 9),
(10, 'Zágráb Főtér Hotel', 'Trg bana Jelačića 3.', NULL, NULL, 10),
(11, 'Temze Parti Szálló', 'Victoria Embankment 1.', NULL, NULL, 11),
(12, 'Amszterdam Csatorna Hotel', 'Herengracht 15.', NULL, NULL, 12),
(13, 'Brüsszel Atomium Hotel', 'Square de l Atomium 1.', NULL, NULL, 13),
(14, 'Lisszabon Óceán Szálló', 'Praça do Comércio 5.', NULL, NULL, 14),
(15, 'Akropolisz Panzió', 'Dionysiou Areopagitou 10.', NULL, NULL, 15),
(16, 'Stockholm Királyi Hotel', 'Slottsbacken 1.', NULL, NULL, 16),
(17, 'Oslo Fjord Szálló', 'Aker Brygge 5.', NULL, NULL, 17),
(18, 'Kis Hableány Hotel', 'Langelinie 1.', NULL, NULL, 18),
(19, 'Helsinki Kikötő Panzió', 'Kauppatori 2.', NULL, NULL, 19),
(20, 'Dublin Lóhere Szálló', 'O Connell Street 10.', NULL, NULL, 20),
(21, 'Bern Medve Hotel', 'Bärenplatz 1.', NULL, NULL, 21),
(22, 'Bukarest Palota Szálló', 'Calea Victoriei 15.', NULL, NULL, 22),
(23, 'Szófia Balkán Hotel', 'Vitosha Blvd 20.', NULL, NULL, 23),
(24, 'Ljubljana Sárkány Panzió', 'Prešernov trg 1.', NULL, NULL, 24),
(25, 'Tallinn Óváros Szálló', 'Raekoja plats 5.', NULL, NULL, 25),
(26, 'Riga Központ Hotel', 'Brīvības iela 10.', NULL, NULL, 26),
(27, 'Vilnius Vár Panzió', 'Pilies g. 5.', NULL, NULL, 27),
(28, 'Luxembourg Nagyherceg Szálló', 'Place Guillaume II 1.', NULL, NULL, 28),
(29, 'Valletta Tengerparti Hotel', 'Republic Street 15.', NULL, NULL, 29),
(30, 'Reykjavík Gejzír Szálló', 'Laugavegur 10.', NULL, NULL, 30),
(31, 'Tirana Sas Hotel', 'Skanderbeg tér 1.', NULL, NULL, 31),
(32, 'Szarajevó Híd Panzió', 'Baščaršija 5.', NULL, NULL, 32),
(33, 'Podgorica Főváros Hotel', 'Hercegovačka 10.', NULL, NULL, 33),
(34, 'Szkopje Kőhíd Szálló', 'Macedónia tér 1.', NULL, NULL, 34),
(35, 'Belgrád Nándorfehérvár Hotel', 'Knez Mihailova 15.', NULL, NULL, 35),
(36, 'Kisinyov Bor Hotel', 'Ștefan cel Mare 10.', NULL, NULL, 36),
(37, 'Kijev Aranykapu Szálló', 'Khreshchatyk 1.', NULL, NULL, 37),
(38, 'Nicosia Sziget Panzió', 'Ledra Street 5.', NULL, NULL, 38),
(39, 'Monaco Kaszinó Hotel', 'Place du Casino 1.', NULL, NULL, 39),
(40, 'Andorra Hegyvidék Szálló', 'Meritxell 10.', NULL, NULL, 40),
(41, 'Vaduz Hercegi Panzió', 'Städtle 5.', NULL, NULL, 41),
(42, 'San Marino Vár Hotel', 'Piazza della Libertà 1.', NULL, NULL, 42),
(43, 'Vatikán Zarándok Szálló', 'Piazza San Pietro 1.', NULL, NULL, 43),
(44, 'Pristina Újváros Hotel', 'Nënë Tereza 10.', NULL, NULL, 44);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `city`
--

CREATE TABLE `city` (
  `city_id` int(11) NOT NULL,
  `name` varchar(100) NOT NULL,
  `zip_code` int(11) NOT NULL,
  `has_airport` tinyint(1) NOT NULL,
  `country_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- A tábla adatainak kiíratása `city`
--

INSERT INTO `city` (`city_id`, `name`, `zip_code`, `has_airport`, `country_id`) VALUES
(1, 'Budapest', 1051, 1, 1),
(2, 'Berlin', 10115, 1, 2),
(3, 'Párizs', 75001, 1, 3),
(4, 'Róma', 100, 1, 4),
(5, 'Madrid', 28001, 1, 5),
(6, 'Bécs', 1010, 1, 6),
(7, 'Varsó', 1, 1, 7),
(8, 'Prága', 11000, 1, 8),
(9, 'Pozsony', 81101, 1, 9),
(10, 'Zágráb', 10000, 1, 10),
(11, 'London', 1000, 1, 11),
(12, 'Amszterdam', 1011, 1, 12),
(13, 'Brüsszel', 1000, 1, 13),
(14, 'Lisszabon', 1000, 1, 14),
(15, 'Athén', 10431, 1, 15),
(16, 'Stockholm', 10316, 1, 16),
(17, 'Oslo', 101, 1, 17),
(18, 'Koppenhága', 1000, 1, 18),
(19, 'Helsinki', 100, 1, 19),
(20, 'Dublin', 2, 1, 20),
(21, 'Bern', 3000, 0, 21),
(22, 'Bukarest', 10001, 1, 22),
(23, 'Szófia', 1000, 1, 23),
(24, 'Ljubljana', 1000, 1, 24),
(25, 'Tallinn', 10111, 1, 25),
(26, 'Riga', 1001, 1, 26),
(27, 'Vilnius', 1001, 1, 27),
(28, 'Luxemburg', 1009, 1, 28),
(29, 'Valletta', 1000, 1, 29),
(30, 'Reykjavík', 101, 1, 30),
(31, 'Tirana', 1001, 1, 31),
(32, 'Szarajevó', 71000, 1, 32),
(33, 'Podgorica', 81000, 1, 33),
(34, 'Szkopje', 1000, 1, 34),
(35, 'Belgrád', 11000, 1, 35),
(36, 'Kisinyov', 2000, 1, 36),
(37, 'Kijev', 1001, 1, 37),
(38, 'Nikózia', 1000, 1, 38),
(39, 'Monaco', 98000, 0, 39),
(40, 'Andorra Vella', 500, 0, 40),
(41, 'Vaduz', 9490, 0, 41),
(42, 'San Marino', 47890, 0, 42),
(43, 'Vatikán', 120, 0, 43),
(44, 'Pristina', 10000, 1, 44);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `country`
--

CREATE TABLE `country` (
  `country_id` int(11) NOT NULL,
  `name` varchar(100) NOT NULL,
  `language` varchar(50) NOT NULL,
  `phone_number` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- A tábla adatainak kiíratása `country`
--

INSERT INTO `country` (`country_id`, `name`, `language`, `phone_number`) VALUES
(1, 'Magyarország', 'magyar', '36'),
(2, 'Németország', 'német', '49'),
(3, 'Franciaország', 'francia', '33'),
(4, 'Olaszország', 'olasz', '39'),
(5, 'Spanyolország', 'spanyol', '34'),
(6, 'Ausztria', 'német', '43'),
(7, 'Lengyelország', 'lengyel', '48'),
(8, 'Csehország', 'cseh', '420'),
(9, 'Szlovákia', 'szlovák', '421'),
(10, 'Horvátország', 'horvát', '385'),
(11, 'Egyesült Királyság', 'angol', '44'),
(12, 'Hollandia', 'holland', '31'),
(13, 'Belgium', 'francia/holland', '32'),
(14, 'Portugália', 'portugál', '351'),
(15, 'Görögország', 'görög', '30'),
(16, 'Svédország', 'svéd', '46'),
(17, 'Norvégia', 'norvég', '47'),
(18, 'Dánia', 'dán', '45'),
(19, 'Finnország', 'finn', '358'),
(20, 'Írország', 'ír/angol', '353'),
(21, 'Svájc', 'német/francia', '41'),
(22, 'Románia', 'román', '40'),
(23, 'Bulgária', 'bolgár', '359'),
(24, 'Szlovénia', 'szlovén', '386'),
(25, 'Észtország', 'észt', '372'),
(26, 'Lettország', 'lett', '371'),
(27, 'Litvánia', 'litván', '370'),
(28, 'Luxemburg', 'luxemburgi', '352'),
(29, 'Málta', 'máltai', '356'),
(30, 'Izland', 'izlandi', '354'),
(31, 'Albánia', 'albán', '355'),
(32, 'Bosznia-Hercegovina', 'bosnyák', '387'),
(33, 'Montenegró', 'montenegrói', '382'),
(34, 'Észak-Macedónia', 'macedón', '389'),
(35, 'Szerbia', 'szerb', '381'),
(36, 'Moldova', 'moldáv', '373'),
(37, 'Ukrajna', 'ukrán', '380'),
(38, 'Ciprus', 'görög/török', '357'),
(39, 'Monaco', 'francia', '377'),
(40, 'Andorra', 'katalán', '376'),
(41, 'Liechtenstein', 'német', '423'),
(42, 'San Marino', 'olasz', '378'),
(43, 'Vatikán', 'olasz/latin', '379'),
(44, 'Koszovó', 'albán', '383');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `restaurant`
--

CREATE TABLE `restaurant` (
  `restaurant_id` int(11) NOT NULL,
  `name` varchar(100) NOT NULL,
  `address` varchar(150) NOT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `image` varchar(100) DEFAULT NULL,
  `city_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- A tábla adatainak kiíratása `restaurant`
--

INSERT INTO `restaurant` (`restaurant_id`, `name`, `address`, `phone`, `image`, `city_id`) VALUES
(1, 'Gundel Étterem', 'Állatkerti út 2.', NULL, NULL, 1),
(2, 'Müncheni Söröző', 'Karl-Liebknecht-Str. 5.', NULL, NULL, 2),
(3, 'Montmartre Bisztró', 'Place du Tertre 1.', NULL, NULL, 3),
(4, 'Trattoria Colosseum', 'Via dei Fori Imperiali 10.', NULL, NULL, 4),
(5, 'Tapas Bár Madrid', 'Gran Vía 15.', NULL, NULL, 5),
(6, 'Bécsi Szelet Ház', 'Wollzeile 5.', NULL, NULL, 6),
(7, 'Varsói Pierogi Falatozó', 'Krakowskie Przedmieście 10.', NULL, NULL, 7),
(8, 'Prágai Svejk Söröző', 'U Radnice 2.', NULL, NULL, 8),
(9, 'Pozsonyi Sztrapacska', 'Michalská 5.', NULL, NULL, 9),
(10, 'Zágrábi Grill Terasz', 'Tkalčićeva 15.', NULL, NULL, 10),
(11, 'London Fish & Chips', 'Covent Garden 1.', NULL, NULL, 11),
(12, 'Amszterdam Sajtozó', 'Dam tér 5.', NULL, NULL, 12),
(13, 'Brüsszeli Gofri Ház', 'Grand-Place 1.', NULL, NULL, 13),
(14, 'Lisszabon Halászcsárda', 'Rua Augusta 10.', NULL, NULL, 14),
(15, 'Athéni Gyros Bár', 'Monastiraki tér 1.', NULL, NULL, 15),
(16, 'Stockholmi Húsgolyó Étterem', 'Gamla Stan 5.', NULL, NULL, 16),
(17, 'Oslo Lazac Bár', 'Karl Johans gate 10.', NULL, NULL, 17),
(18, 'Koppenhága Smørrebrød', 'Nyhavn 15.', NULL, NULL, 18),
(19, 'Helsinki Rénszarvas Grill', 'Mannerheimintie 5.', NULL, NULL, 19),
(20, 'Dublin Ír Pub', 'Temple Bar 1.', NULL, NULL, 20),
(21, 'Berni Fondue Ház', 'Kramgasse 10.', NULL, NULL, 21),
(22, 'Bukaresti Miccs Falatozó', 'Lipscani 5.', NULL, NULL, 22),
(23, 'Szófiai Sopszka Terasz', 'Tsar Osvoboditel 10.', NULL, NULL, 23),
(24, 'Ljubljanai Piac Bisztró', 'Vodnikov trg 1.', NULL, NULL, 24),
(25, 'Tallinn Középkori Lakoma', 'Viru tänav 5.', NULL, NULL, 25),
(26, 'Rigai Fekete Balzsam Bár', 'Kalku iela 10.', NULL, NULL, 26),
(27, 'Vilniusi Cepelinai Ház', 'Gedimino prospektas 1.', NULL, NULL, 27),
(28, 'Luxembourg Francia Étterem', 'Rue des Bains 5.', NULL, NULL, 28),
(29, 'Valletta Tengeri Konyha', 'Merchant Street 10.', NULL, NULL, 29),
(30, 'Reykjavík Bálna Étterem', 'Skólavörðustígur 5.', NULL, NULL, 30),
(31, 'Tiranai Burek Sütő', 'Bulevardi Dëshmorët 1.', NULL, NULL, 31),
(32, 'Szarajevói Ćevapi Kert', 'Ferhadija 10.', NULL, NULL, 32),
(33, 'Podgorica Grill', 'Njegoševa 5.', NULL, NULL, 33),
(34, 'Szkopjei Kebab Ház', 'Old Bazaar 1.', NULL, NULL, 34),
(35, 'Belgrádi Pljeskavica', 'Skadarlija 10.', NULL, NULL, 35),
(36, 'Kisinyov Moldáv Konyha', 'Puskin utca 5.', NULL, NULL, 36),
(37, 'Kijevi Borscs Étterem', 'Andriivskyi Descent 1.', NULL, NULL, 37),
(38, 'Nicosia Halloumi Bár', 'Makariou Ave 10.', NULL, NULL, 38),
(39, 'Monaco Luxus Étterem', 'Avenue Princesse Grace 1.', NULL, NULL, 39),
(40, 'Andorrai Pásztor Vendéglő', 'Avinguda Carlemany 5.', NULL, NULL, 40),
(41, 'Vaduzi Alpesi Étterem', 'Äulestraße 10.', NULL, NULL, 41),
(42, 'San Marino Pizzéria', 'Via Donna Felicissima 1.', NULL, NULL, 42),
(43, 'Vatikáni Trattoria', 'Borgo Pio 5.', NULL, NULL, 43),
(44, 'Pristinai Macchiato Kávézó', 'Agim Ramadani 10.', NULL, NULL, 44);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `restauranttable`
--

CREATE TABLE `restauranttable` (
  `table_id` int(11) NOT NULL,
  `restaurant_id` int(11) NOT NULL,
  `seats` int(11) NOT NULL DEFAULT 2
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `room`
--

CREATE TABLE `room` (
  `room_id` int(11) NOT NULL,
  `accommodation_id` int(11) NOT NULL,
  `room_number` varchar(10) DEFAULT NULL,
  `capacity` int(11) NOT NULL DEFAULT 1,
  `price` decimal(10,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `roombooking`
--

CREATE TABLE `roombooking` (
  `booking_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `room_id` int(11) NOT NULL,
  `check_in` datetime NOT NULL,
  `check_out` datetime NOT NULL,
  `rating` int(11) DEFAULT NULL,
  `created_at` datetime DEFAULT current_timestamp(),
  `status` enum('free','booked') DEFAULT 'free'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `tablereservation`
--

CREATE TABLE `tablereservation` (
  `reservation_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `table_id` int(11) NOT NULL,
  `reservation_start` datetime NOT NULL,
  `reservation_end` datetime NOT NULL,
  `created_at` datetime DEFAULT current_timestamp(),
  `status` enum('free','booked') DEFAULT 'free'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `user`
--

CREATE TABLE `user` (
  `user_id` int(11) NOT NULL,
  `name` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `password` varchar(100) NOT NULL,
  `is_admin` tinyint(1) NOT NULL,
  `token` varchar(1024) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- A tábla adatainak kiíratása `user`
--

INSERT INTO `user` (`user_id`, `name`, `email`, `phone`, `password`, `is_admin`, `token`) VALUES
(5, 'Mucza János', 'muczaj9@gmail.com', '+36306397871', '9Rkm/PLi2IhQNDiToGnVMA==.75KTa8HtGaP3N7A9raVWf5ocv9zgVBVM7wTiWbc+xVw=', 1, NULL),
(6, 'mucza', '123@gmail.com', '+36306397871', 'dW4U6bhVJsrsM4rPDryukQ==.q/SpqnLaQnl6LFYnij78vx6HXWI35votpfPlLoyBVck=', 0, NULL),
(8, 'Németh Áron', 'kissgeza@gmail.com', '+363063921312', 'hl/rkI26qbcBGgJMhmJMkw==.ynn3wj5Zc5wHxyNLNtgFkp1N/02xtqf7E10zhOlWJTo=', 0, NULL);

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `accommodation`
--
ALTER TABLE `accommodation`
  ADD PRIMARY KEY (`accommodation_id`),
  ADD KEY `city_id` (`city_id`);

--
-- A tábla indexei `city`
--
ALTER TABLE `city`
  ADD PRIMARY KEY (`city_id`),
  ADD KEY `country_id` (`country_id`);

--
-- A tábla indexei `country`
--
ALTER TABLE `country`
  ADD PRIMARY KEY (`country_id`);

--
-- A tábla indexei `restaurant`
--
ALTER TABLE `restaurant`
  ADD PRIMARY KEY (`restaurant_id`),
  ADD KEY `city_id` (`city_id`);

--
-- A tábla indexei `restauranttable`
--
ALTER TABLE `restauranttable`
  ADD PRIMARY KEY (`table_id`),
  ADD KEY `restaurant_id` (`restaurant_id`);

--
-- A tábla indexei `room`
--
ALTER TABLE `room`
  ADD PRIMARY KEY (`room_id`),
  ADD KEY `accommodation_id` (`accommodation_id`);

--
-- A tábla indexei `roombooking`
--
ALTER TABLE `roombooking`
  ADD PRIMARY KEY (`booking_id`),
  ADD KEY `user_id` (`user_id`),
  ADD KEY `room_id` (`room_id`),
  ADD KEY `idx_roombooking_room_time` (`room_id`,`check_in`,`check_out`);

--
-- A tábla indexei `tablereservation`
--
ALTER TABLE `tablereservation`
  ADD PRIMARY KEY (`reservation_id`),
  ADD KEY `user_id` (`user_id`),
  ADD KEY `table_id` (`table_id`),
  ADD KEY `idx_tablereservation_table_time` (`table_id`,`reservation_start`,`reservation_end`);

--
-- A tábla indexei `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`user_id`),
  ADD UNIQUE KEY `email` (`email`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `user`
--
ALTER TABLE `user`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `accommodation`
--
ALTER TABLE `accommodation`
  ADD CONSTRAINT `accommodation_ibfk_1` FOREIGN KEY (`city_id`) REFERENCES `city` (`city_id`);

--
-- Megkötések a táblához `city`
--
ALTER TABLE `city`
  ADD CONSTRAINT `city_ibfk_1` FOREIGN KEY (`country_id`) REFERENCES `country` (`country_id`);

--
-- Megkötések a táblához `restaurant`
--
ALTER TABLE `restaurant`
  ADD CONSTRAINT `restaurant_ibfk_1` FOREIGN KEY (`city_id`) REFERENCES `city` (`city_id`);

--
-- Megkötések a táblához `restauranttable`
--
ALTER TABLE `restauranttable`
  ADD CONSTRAINT `restauranttable_ibfk_1` FOREIGN KEY (`restaurant_id`) REFERENCES `restaurant` (`restaurant_id`);

--
-- Megkötések a táblához `room`
--
ALTER TABLE `room`
  ADD CONSTRAINT `room_ibfk_1` FOREIGN KEY (`accommodation_id`) REFERENCES `accommodation` (`accommodation_id`);

--
-- Megkötések a táblához `roombooking`
--
ALTER TABLE `roombooking`
  ADD CONSTRAINT `roombooking_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`),
  ADD CONSTRAINT `roombooking_ibfk_2` FOREIGN KEY (`room_id`) REFERENCES `room` (`room_id`);

--
-- Megkötések a táblához `tablereservation`
--
ALTER TABLE `tablereservation`
  ADD CONSTRAINT `tablereservation_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`),
  ADD CONSTRAINT `tablereservation_ibfk_2` FOREIGN KEY (`table_id`) REFERENCES `restauranttable` (`table_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
