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

  INSERT INTO Flight VALUES ('BA 98', SYSDATETIMEOFFSET(), DATEADD(day, 1, SYSDATETIMEOFFSET()), 'YYZ', 'LHR', 0, 227.81, 266.5377);

  CREATE UNIQUE INDEX  idx_filter_iata ON Flight (departureDateTime ASC, arrivalAirport, departureAirport);
CREATE UNIQUE INDEX  idx_filter_price ON Flight (departureDateTime ASC, arrivalDateTime ASC, totalPriceNIS);
CREATE UNIQUE INDEX  idx_filter_type ON Flight (departureDateTime ASC, arrivalDateTime ASC, flightType);
