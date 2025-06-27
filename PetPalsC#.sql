create database PetPalsDb
use PetPalsDb

CREATE TABLE Donations (
    DonationId INT PRIMARY KEY IDENTITY,
    DonorName NVARCHAR(100),
    DonationDate DATETIME,
    Amount real,
    ItemType NVARCHAR(100) NULL
);


CREATE TABLE Pets (
    PetId INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100),
    Age INT,
    Breed NVARCHAR(100)
);

CREATE TABLE Dogs (
    DogId INT PRIMARY KEY FOREIGN KEY REFERENCES Pets(PetId),
    DogBreed NVARCHAR(100)
);

CREATE TABLE Cats (
    CatId INT PRIMARY KEY FOREIGN KEY REFERENCES Pets(PetId),
    CatColor NVARCHAR(100)
);

select * from pets
select * from cats
select * from dogs
select * from AdoptionEvents --ef
select * from Participants --ef
select * from Donations

INSERT INTO AdoptionEvents (EventName, EventDate)
VALUES 
('June Pet Carnival', '2025-07-01'),
('Monsoon Rescue Drive', '2025-07-10'),
('Independence Day Adoption Camp', '2025-08-15');

ALTER TABLE Pets ADD Adopted BIT DEFAULT 0;
UPDATE Pets SET Adopted = 0 WHERE Adopted IS NULL;

