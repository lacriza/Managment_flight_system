USE ISSTA_Exercise;

CREATE TABLE Flight (
  flightNumber VARCHAR(10) NOT NULL PRIMARY KEY,
  departureDateTime datetimeoffset NOT NULL,
  arrivalDateTime datetimeoffset NOT NULL,
  arrivalAirport CHAR(3) NOT NULL FOREIGN KEY REFERENCES Airport(code),
  departureAirport CHAR(3) NOT NULL FOREIGN KEY REFERENCES Airport(code),
  flightType INT NOT NULL,
  basePriceNIS money NOT NULL,
  totalPriceNIS money NOT NULL,
  CONSTRAINT arrivalAirport CHECK (arrivalAirport != departureAirport)
  );


CREATE UNIQUE INDEX  idx_filter_iata ON Flight (departureDateTime ASC, arrivalAirport, departureAirport);
CREATE UNIQUE INDEX  idx_filter_price ON Flight (departureDateTime ASC, arrivalDateTime ASC, totalPriceNIS);
CREATE UNIQUE INDEX  idx_filter_type ON Flight (departureDateTime ASC, arrivalDateTime ASC, flightType);

USE [ISSTA_Exercise]
GO
INSERT [dbo].[Flight] ([flightNumber], [departureDateTime], [arrivalDateTime], [arrivalAirport], [departureAirport], [flightType], [basePriceNIS], [totalPriceNIS]) VALUES (N'7W164', CAST(N'2022-08-02T08:16:41.2940000+00:00' AS DateTimeOffset), CAST(N'2022-08-02T08:16:41.2940000+00:00' AS DateTimeOffset), N'KBP', N'LVO', 0, 0.0000, 205.3350)
INSERT [dbo].[Flight] ([flightNumber], [departureDateTime], [arrivalDateTime], [arrivalAirport], [departureAirport], [flightType], [basePriceNIS], [totalPriceNIS]) VALUES (N'7W6578', CAST(N'2021-07-30T09:47:21.4589903+03:00' AS DateTimeOffset), CAST(N'2021-08-02T09:47:21.4589903+03:00' AS DateTimeOffset), N'KBP', N'LVO', 1, 100.0000, 90.0000)
INSERT [dbo].[Flight] ([flightNumber], [departureDateTime], [arrivalDateTime], [arrivalAirport], [departureAirport], [flightType], [basePriceNIS], [totalPriceNIS]) VALUES (N'7W68', CAST(N'2021-07-30T09:48:18.1821170+03:00' AS DateTimeOffset), CAST(N'2021-07-31T09:48:18.1821170+03:00' AS DateTimeOffset), N'KBP', N'YYZ', 1, 100.0000, 90.0000)
INSERT [dbo].[Flight] ([flightNumber], [departureDateTime], [arrivalDateTime], [arrivalAirport], [departureAirport], [flightType], [basePriceNIS], [totalPriceNIS]) VALUES (N'AA641', CAST(N'2021-08-08T15:54:05.9030000+00:00' AS DateTimeOffset), CAST(N'2021-08-12T19:54:05.9030000+00:00' AS DateTimeOffset), N'ARC', N'AFA', 1, 600.0000, 900.0000)
INSERT [dbo].[Flight] ([flightNumber], [departureDateTime], [arrivalDateTime], [arrivalAirport], [departureAirport], [flightType], [basePriceNIS], [totalPriceNIS]) VALUES (N'AMLV102', CAST(N'2021-09-01T15:50:13.3040000+00:00' AS DateTimeOffset), CAST(N'2021-08-09T15:50:13.3040000+00:00' AS DateTimeOffset), N'AAM', N'TLV', 0, 500.0000, 585.0000)
INSERT [dbo].[Flight] ([flightNumber], [departureDateTime], [arrivalDateTime], [arrivalAirport], [departureAirport], [flightType], [basePriceNIS], [totalPriceNIS]) VALUES (N'BA 98', CAST(N'2021-07-27T18:34:55.3875180+03:00' AS DateTimeOffset), CAST(N'2021-07-28T18:34:55.3875180+03:00' AS DateTimeOffset), N'YYZ', N'LHR', 0, 227.8100, 266.5377)
INSERT [dbo].[Flight] ([flightNumber], [departureDateTime], [arrivalDateTime], [arrivalAirport], [departureAirport], [flightType], [basePriceNIS], [totalPriceNIS]) VALUES (N'BA125', CAST(N'2021-07-30T09:47:21.4550040+03:00' AS DateTimeOffset), CAST(N'2021-08-03T09:47:21.4550040+03:00' AS DateTimeOffset), N'YYZ', N'LHR', 0, 227.8100, 266.5377)
INSERT [dbo].[Flight] ([flightNumber], [departureDateTime], [arrivalDateTime], [arrivalAirport], [departureAirport], [flightType], [basePriceNIS], [totalPriceNIS]) VALUES (N'BA878', CAST(N'2021-07-30T09:48:18.1821170+03:00' AS DateTimeOffset), CAST(N'2021-08-01T09:48:18.1821170+03:00' AS DateTimeOffset), N'KBP', N'LHR', 0, 227.8100, 266.5377)
INSERT [dbo].[Flight] ([flightNumber], [departureDateTime], [arrivalDateTime], [arrivalAirport], [departureAirport], [flightType], [basePriceNIS], [totalPriceNIS]) VALUES (N'FR5126', CAST(N'2021-07-28T19:17:33.2180746+03:00' AS DateTimeOffset), CAST(N'2021-07-29T19:17:33.2180746+03:00' AS DateTimeOffset), N'KBP', N'BCN', 0, 107.0000, 256.0000)
INSERT [dbo].[Flight] ([flightNumber], [departureDateTime], [arrivalDateTime], [arrivalAirport], [departureAirport], [flightType], [basePriceNIS], [totalPriceNIS]) VALUES (N'PS53', CAST(N'2021-07-28T19:21:20.8460526+03:00' AS DateTimeOffset), CAST(N'2021-07-29T19:21:20.8460526+03:00' AS DateTimeOffset), N'KBP', N'ODS', 2, 50.0000, 62.5000)
INSERT [dbo].[Flight] ([flightNumber], [departureDateTime], [arrivalDateTime], [arrivalAirport], [departureAirport], [flightType], [basePriceNIS], [totalPriceNIS]) VALUES (N'PS811', CAST(N'2021-07-30T09:48:18.1831141+03:00' AS DateTimeOffset), CAST(N'2021-08-02T09:48:18.1831141+03:00' AS DateTimeOffset), N'YYZ', N'ODS', 2, 50.0000, 62.5000)
INSERT [dbo].[Flight] ([flightNumber], [departureDateTime], [arrivalDateTime], [arrivalAirport], [departureAirport], [flightType], [basePriceNIS], [totalPriceNIS]) VALUES (N'PS848', CAST(N'2021-07-30T09:47:21.4589903+03:00' AS DateTimeOffset), CAST(N'2021-08-01T09:47:21.4589903+03:00' AS DateTimeOffset), N'KBP', N'ODS', 2, 50.0000, 62.5000)
GO
