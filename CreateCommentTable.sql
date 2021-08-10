USE ISSTA_Exercise;

CREATE TABLE Comments (
  commentId INT NOT NULL,
  flightType INT NOT NULL,
  comment VARCHAR(255) NOT NULL, 
  PRIMARY KEY (commentId), 
  );

  CREATE INDEX  idx_flightType ON Comments (flightType);

  INSERT INTO Comments VALUES (1, 0, 'This flight includes seating.');
  INSERT INTO Comments VALUES (2, 0, 'This flight is approved by the Airports Authority.');

  INSERT INTO Comments VALUES (3, 1, 'This flight does not include luggage.');
  INSERT INTO Comments VALUES (4, 1, 'This flight does not include meals.');
  INSERT INTO Comments VALUES (5, 1, 'For this flight, full cancellation fee.');

  INSERT INTO Comments VALUES (6, 2, 'Departure time for this flight may vary.');
  INSERT INTO Comments VALUES (7, 2, 'Selection of a seat for this flight will only be possible at the check-in desk.');