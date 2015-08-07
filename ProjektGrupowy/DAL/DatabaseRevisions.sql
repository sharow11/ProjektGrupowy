----------------------------[REV 1]---------------------------------
-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2015-07-12 17:31:25.529

-- Table: User
CREATE TABLE User (
    id integer NOT NULL  PRIMARY KEY AUTOINCREMENT,
    Name varchar(255) NOT NULL,
    Banned boolean NOT NULL,
    DateRegistered datetime NOT NULL,
    Email varchar(255) NOT NULL,
    EmailConfirmed boolean NOT NULL,
    PasswordHash varchar(255) NOT NULL,
    SecurityStamp varchar(255) NOT NULL,
    BirthDate datetime NOT NULL
);

-- tables
-- Table: Comment
CREATE TABLE Comment (
    id integer NOT NULL  PRIMARY KEY,
    TimePosted datetime NOT NULL,
    Deleted boolean NOT NULL,
    UserId integer NOT NULL,
    IdeaId integer NOT NULL,
    ParentId integer,
    FOREIGN KEY (ParentId) REFERENCES Comment (id),
    FOREIGN KEY (IdeaId) REFERENCES Idea (id),
    FOREIGN KEY (UserId) REFERENCES User (id)
);

-- Table: CommentVote
CREATE TABLE CommentVote (
    id integer NOT NULL  PRIMARY KEY,
    Up boolean NOT NULL,
    UserId integer NOT NULL,
    CommentId integer NOT NULL,
    TimePosted datetime NOT NULL,
    FOREIGN KEY (UserId) REFERENCES User (id),
    FOREIGN KEY (CommentId) REFERENCES Comment (id)
);

-- Table: Idea
CREATE TABLE Idea (
    id integer NOT NULL  PRIMARY KEY AUTOINCREMENT,
    Deleted boolean NOT NULL,
    Title varchar(255) NOT NULL,
    Description text NOT NULL,
    UserId integer NOT NULL,
    TimePosted datetime NOT NULL,
    TimeValidated datetime,
    TimeClosed datetime,
    FOREIGN KEY (UserId) REFERENCES User (id)
);

-- Table: IdeaVote
CREATE TABLE IdeaVote (
    id integer NOT NULL  PRIMARY KEY,
    Up boolean NOT NULL,
    Time_Posted datetime NOT NULL,
    Idea_id integer,
    User_id integer NOT NULL,
    FOREIGN KEY (IdeaId) REFERENCES Idea (id),
    FOREIGN KEY (UserId) REFERENCES User (id)
);

-- Table: Idea_is_Tagged
CREATE TABLE IdeaIsTagged (
    id integer NOT NULL  PRIMARY KEY,
    IdeaId integer NOT NULL,
    TagId integer NOT NULL,
    FOREIGN KEY (IdeaId) REFERENCES Idea (id),
    FOREIGN KEY (TagId) REFERENCES Tag (id)
);

-- Table: Tag
CREATE TABLE Tag (
    id integer NOT NULL  PRIMARY KEY,
    Name varchar(64) NOT NULL,
    AddedBy integer NOT NULL,
    TimeCreated datetime NOT NULL,
    Deleted boolean NOT NULL,
    CreatorId integer NOT NULL,
    FOREIGN KEY (CreatorId) REFERENCES User (id)
);

-- Table: User_observes_Tag
CREATE TABLE UserObservesTag (
    id integer NOT NULL  PRIMARY KEY,
    TagId integer NOT NULL,
    UserId integer NOT NULL,
    TimeCreated datetime NOT NULL,
    FOREIGN KEY (TagId) REFERENCES Tag (id),
    FOREIGN KEY (UserId) REFERENCES User (id)
);





-- End of file.

