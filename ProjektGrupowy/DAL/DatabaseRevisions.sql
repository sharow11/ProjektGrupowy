----------------------------[REV 1]---------------------------------
-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2015-07-12 17:31:25.529

-- Table: User
CREATE TABLE Users (
    id integer NOT NULL  PRIMARY KEY AUTOINCREMENT,
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
    id integer NOT NULL  PRIMARY KEY AUTOINCREMENT,
    Deleted boolean NOT NULL DEFAULT FALSE,
    Title varchar NOT NULL,
    Description text NOT NULL,
    UserId integer NOT NULL,
    TimePosted datetime NOT NULL,
    TimeValidated datetime,
    TimeClosed datetime,
    FOREIGN KEY (UserId) REFERENCES Users (id)
);

-- tables
-- Table: Comment
CREATE TABLE Comments (
    id integer NOT NULL  PRIMARY KEY,
    TimePosted datetime NOT NULL,
    Deleted boolean NOT NULL,
    UserId integer NOT NULL,
    IdeaId integer NOT NULL,
    ParentId integer,
    FOREIGN KEY (ParentId) REFERENCES Comments (id),
    FOREIGN KEY (IdeaId) REFERENCES Ideas (id),
    FOREIGN KEY (UserId) REFERENCES Users (id)
);

-- Table: CommentVote
CREATE TABLE CommentVotes (
    id integer NOT NULL  PRIMARY KEY,
    Up boolean NOT NULL,
    UserId integer NOT NULL,
    CommentId integer NOT NULL,
    TimePosted datetime NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users (id),
    FOREIGN KEY (CommentId) REFERENCES Comments (id)
);

-- Table: IdeaVote
CREATE TABLE IdeaVotes (
    id integer NOT NULL  PRIMARY KEY,
    Up boolean NOT NULL,
    Time_Posted datetime NOT NULL,
    IdeaId integer,
    UserId integer NOT NULL,
    FOREIGN KEY (IdeaId) REFERENCES Ideas (id),
    FOREIGN KEY (UserId) REFERENCES Users (id)
);

-- Table: Tag
CREATE TABLE Tags (
    id integer NOT NULL  PRIMARY KEY,
    Name varchar NOT NULL,
    TimeCreated datetime NOT NULL,
    Deleted boolean NOT NULL,
    CreatorId integer NOT NULL,
    FOREIGN KEY (CreatorId) REFERENCES Users (id)
);

-- Table: Idea_is_Tagged
CREATE TABLE IdeasAreTagged (
    id integer NOT NULL  PRIMARY KEY,
    IdeaId integer NOT NULL,
    TagId integer NOT NULL,
    FOREIGN KEY (IdeaId) REFERENCES Ideas (id),
    FOREIGN KEY (TagId) REFERENCES Tags (id)
);

-- Table: User_observes_Tag
CREATE TABLE UserObservesTags (
    id integer NOT NULL  PRIMARY KEY,
    TagId integer NOT NULL,
    UserId integer NOT NULL,
    TimeCreated datetime NOT NULL,
    FOREIGN KEY (TagId) REFERENCES Tags (id),
    FOREIGN KEY (UserId) REFERENCES Users (id)
);


-- End of file.

