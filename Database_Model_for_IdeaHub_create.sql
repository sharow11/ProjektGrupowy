-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2015-07-12 17:31:25.529



-- tables
-- Table: Comment
CREATE TABLE Comment (
    id integer NOT NULL  PRIMARY KEY,
    time_posted datetime NOT NULL,
    deleted boolean NOT NULL,
    User_id integer NOT NULL,
    Idea_id integer NOT NULL,
    Parent_id integer,
    FOREIGN KEY (Parent_id) REFERENCES Comment (id),
    FOREIGN KEY (Idea_id) REFERENCES Idea (id),
    FOREIGN KEY (User_id) REFERENCES User (id)
);

-- Table: CommentVote
CREATE TABLE CommentVote (
    id integer NOT NULL  PRIMARY KEY,
    up boolean NOT NULL,
    User_id integer NOT NULL,
    Comment_id integer NOT NULL,
    time_posted datetime NOT NULL,
    FOREIGN KEY (User_id) REFERENCES User (id),
    FOREIGN KEY (Comment_id) REFERENCES Comment (id)
);

-- Table: Idea
CREATE TABLE Idea (
    id integer NOT NULL  PRIMARY KEY AUTOINCREMENT,
    deleted boolean NOT NULL,
    title varchar(255) NOT NULL,
    description text NOT NULL,
    User_id integer NOT NULL,
    time_posted datetime NOT NULL,
    time_validated datetime,
    time_closed datetime,
    FOREIGN KEY (User_id) REFERENCES User (id)
);

-- Table: IdeaVote
CREATE TABLE IdeaVote (
    id integer NOT NULL  PRIMARY KEY,
    up boolean NOT NULL,
    time_posted datetime NOT NULL,
    Idea_id integer,
    User_id integer NOT NULL,
    FOREIGN KEY (Idea_id) REFERENCES Idea (id),
    FOREIGN KEY (User_id) REFERENCES User (id)
);

-- Table: Idea_is_Tagged
CREATE TABLE Idea_is_Tagged (
    id integer NOT NULL  PRIMARY KEY,
    Idea_id integer NOT NULL,
    Tag_id integer NOT NULL,
    FOREIGN KEY (Idea_id) REFERENCES Idea (id),
    FOREIGN KEY (Tag_id) REFERENCES Tag (id)
);

-- Table: Tag
CREATE TABLE Tag (
    id integer NOT NULL  PRIMARY KEY,
    name varchar(64) NOT NULL,
    added_by integer NOT NULL,
    time_created datetime NOT NULL,
    deleted boolean NOT NULL,
    Creator_id integer NOT NULL,
    FOREIGN KEY (Creator_id) REFERENCES User (id)
);

-- Table: User
CREATE TABLE User (
    id integer NOT NULL  PRIMARY KEY AUTOINCREMENT,
    name varchar(255) NOT NULL,
    banned boolean NOT NULL,
    time_registered datetime NOT NULL,
    email varchar(255) NOT NULL,
    email_confirmed boolean NOT NULL,
    password_hash varchar(255) NOT NULL,
    security_stamp varchar(255) NOT NULL,
    birth_date datetime NOT NULL
);

-- Table: User_observes_Tag
CREATE TABLE User_observes_Tag (
    id integer NOT NULL  PRIMARY KEY,
    Tag_id integer NOT NULL,
    User_id integer NOT NULL,
    time_created datetime NOT NULL,
    FOREIGN KEY (Tag_id) REFERENCES Tag (id),
    FOREIGN KEY (User_id) REFERENCES User (id)
);





-- End of file.

