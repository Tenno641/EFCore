CREATE TABLE IF NOT EXISTS "Movies" (
    "Id" SERIAL PRIMARY KEY,
    "Title" TEXT,
    "ReleaseDate" TIMESTAMP,
    "Synopsis" TEXT
);

INSERT INTO "Movies" ("Title", "ReleaseDate", "Synopsis")
VALUES
    ('The Matrix', '1999-03-31', 'A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.'),
    ('Inception', '2010-07-16', 'A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.'),
    ('The Shawshank Redemption', '1994-09-23', 'Over the course of several years, two convicts form a friendship, seeking consolation and, eventually, redemption through basic compassion.'),
    ('Pulp Fiction', '1994-10-14', 'The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.'),
    ('Forrest Gump', '1994-07-06', 'The history of the United States from the 1950s to the ''70s unfolds from the perspective of an Alabama man with an IQ of 75, who yearns to be reunited with his childhood sweetheart.');