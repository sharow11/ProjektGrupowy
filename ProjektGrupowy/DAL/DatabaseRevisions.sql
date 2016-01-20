----------------------------[REV 1]---------------------------------
-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2015-07-12 17:31:25.529

-- Table: User
CREATE TABLE AspNetUsers (
    Id integer NOT NULL  PRIMARY KEY AUTOINCREMENT,
    UserName varchar NOT NULL,
    LockoutEnabled boolean NOT NULL DEFAULT 0,
	LockoutEndDateUtc datetime,
    DateRegistered datetime NOT NULL,
    Email varchar NOT NULL,
	AccessFailedCount int NOT NULL DEFAULT 0,
    EmailConfirmed boolean NOT NULL DEFAULT 0,
	PhoneNumber varchar,
	PhoneNumberConfirmed boolean,
	TwoFactorEnabled boolean NOT NULL DEFAULT 0,
    PasswordHash varchar NULL,
    SecurityStamp varchar NULL,
    BirthDate datetime NULL
);

CREATE TABLE AspNetUserRoles (
    RoleId integer NOT NULL,
	UserId integer NOT NULL,
	PRIMARY KEY (RoleId, UserId),
    FOREIGN KEY (RoleId) REFERENCES AspNetRoles (id),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers (id)
);

CREATE TABLE AspNetUserClaims (
    Id integer NOT NULL  PRIMARY KEY AUTOINCREMENT,
	ClaimType varchar,
	ClaimValue varchar,
	UserId integer NOT NULL,
	FOREIGN KEY (UserId) REFERENCES AspNetUsers (id)
);

CREATE TABLE AspNetUserLogins (
	LoginProvider varchar,
	ProviderKey varchar,
	UserId integer NOT NULL,
	PRIMARY KEY (UserId),
	FOREIGN KEY (UserId) REFERENCES AspNetUsers (id)
);

CREATE TABLE AspNetRoles (
Id integer NOT NULL  PRIMARY KEY AUTOINCREMENT,
Name varchar
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
    FOREIGN KEY (UserId) REFERENCES AspNetUsers (id)
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
	Score int NOT NULL DEFAULT 0,
    FOREIGN KEY (ParentId) REFERENCES Comments (id),
    FOREIGN KEY (IdeaId) REFERENCES Ideas (id),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers (id)
);

-- Table: Tag
CREATE TABLE Tags (
    Id integer NOT NULL PRIMARY KEY AUTOINCREMENT,
    Name varchar NOT NULL,
    TimeCreated datetime NOT NULL,
    Deleted boolean NOT NULL,
    CreatorId integer NOT NULL,
    FOREIGN KEY (CreatorId) REFERENCES AspNetUsers (id)
);

-- Table: Idea_is_Tagged
CREATE TABLE IdeaTags (
    TagId integer NOT NULL,
	IdeaId int NOT NULL,
	PRIMARY KEY (TagId, IdeaId),
    FOREIGN KEY (IdeaId) REFERENCES Ideas (id),
    FOREIGN KEY (TagId) REFERENCES Tags (id)
);
----------------------------[/REV 1]---------------------------------

----------------------------[REV 2]---------------------------------
CREATE TABLE Votes (
    Id integer NOT NULL PRIMARY KEY AUTOINCREMENT,
	IdeaId int NOT NULL,
	UserId integer NOT NULL,
	VoteValue int NOT NULL DEFAULT(1),
    FOREIGN KEY (IdeaId) REFERENCES Ideas (id),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers (id)
);
----------------------------[/REV 2]---------------------------------

----------------------------[REV 3]----------------------------------
CREATE TABLE UserNotes (
    Id integer NOT NULL PRIMARY KEY AUTOINCREMENT,
	IdeaId int NOT NULL,
	AutorUserId integer NOT NULL,
	RecipientUserId integer NOT NULL,
	Score int NOT NULL DEFAULT(100),
	Comment Text,
	TimePosted datetime NOT NULL,
    FOREIGN KEY (IdeaId) REFERENCES Ideas (id),
    FOREIGN KEY (AutorUserId) REFERENCES AspNetUsers (id)
	FOREIGN KEY (RecipientUserId) REFERENCES AspNetUsers (id)
);
----------------------------[/REV 3]---------------------------------

