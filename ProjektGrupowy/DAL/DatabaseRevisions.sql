----------------------------[REV 1]---------------------------------
-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2015-07-12 17:31:25.529

-- Table: User
CREATE TABLE Users (
    Id integer NOT NULL  PRIMARY KEY AUTOINCREMENT,
    Name varchar NOT NULL,
    Banned boolean NOT NULL DEFAULT 0,
    DateRegistered datetime NOT NULL,
    Email varchar NOT NULL,
    EmailConfirmed boolean NOT NULL DEFAULT 0,
    PasswordHash varchar NOT NULL,
    SecurityStamp varchar NOT NULL,
    BirthDate datetime NOT NULL
);

-- Table: Idea
CREATE TABLE Ideas (
    Id integer NOT NULL  PRIMARY KEY AUTOINCREMENT,
    Deleted boolean NOT NULL DEFAULT FALSE,
    Title varchar NOT NULL,
    Description text NOT NULL,
    UserId integer NOT NULL,
    TimePosted datetime NOT NULL,
    TimeValidated datetime,
    TimeClosed datetime,
	Score int NOT NULL DEFAULT(0),
    FOREIGN KEY (UserId) REFERENCES Users (id)
);

-- tables
-- Table: Comment
CREATE TABLE Comments (
    Id integer NOT NULL  PRIMARY KEY AUTOINCREMENT,
    TimePosted datetime NOT NULL,
    Deleted boolean NOT NULL,
    UserId integer NOT NULL,
    IdeaId integer NOT NULL,
    ParentId integer,
	CommentText Text,
	Score int NOT NULL DEFAULT(0),
    FOREIGN KEY (ParentId) REFERENCES Comments (id),
    FOREIGN KEY (IdeaId) REFERENCES Ideas (id),
    FOREIGN KEY (UserId) REFERENCES Users (id)
);

-- Table: Tag
CREATE TABLE Tags (
    Id integer NOT NULL  PRIMARY KEY AUTOINCREMENT,
    Name varchar NOT NULL,
    TimeCreated datetime NOT NULL,
    Deleted boolean NOT NULL,
    CreatorId integer NOT NULL,
    FOREIGN KEY (CreatorId) REFERENCES Users (id)
);

-- Table: Idea_is_Tagged
CREATE TABLE IdeaTags (
    TagId integer NOT NULL,
	IdeaId int NOT NULL,
	PRIMARY KEY (TagId, IdeaId),
    FOREIGN KEY (IdeaId) REFERENCES Ideas (id),
    FOREIGN KEY (TagId) REFERENCES Tags (id)
);
-- End of file.

