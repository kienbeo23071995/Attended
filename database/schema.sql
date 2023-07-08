CREATE TABLE attended (
  roomid int NOT NULL, 
  slotid int NOT NULL, 
  [date] date NOT NULL, 
  userid int NOT NULL, 
  status bit NOT NULL, 
  PRIMARY KEY (roomid, 
  slotid, 
  [date], 
  userid));
CREATE TABLE class (
  id   int IDENTITY NOT NULL, 
  name nvarchar(2000) NULL, 
  PRIMARY KEY (id));
CREATE TABLE course (
  id   int IDENTITY NOT NULL, 
  name nvarchar(2000) NULL, 
  PRIMARY KEY (id));
CREATE TABLE [group] (
  id   int IDENTITY NOT NULL, 
  name nvarchar(2000) NULL, 
  PRIMARY KEY (id));
CREATE TABLE room (
  id   int IDENTITY NOT NULL, 
  name nvarchar(2000) NULL, 
  PRIMARY KEY (id));
CREATE TABLE [session] (
  roomid   int NOT NULL, 
  slotid   int NOT NULL, 
  [date]   date NOT NULL, 
  courseid int NOT NULL, 
  userid   int NOT NULL, 
  classid  int NOT NULL, 
  PRIMARY KEY (roomid, 
  slotid, 
  [date]));
CREATE TABLE slot (
  id   int IDENTITY NOT NULL, 
  name nvarchar(2000) NULL, 
  PRIMARY KEY (id));
CREATE TABLE [user] (
  id       int IDENTITY NOT NULL, 
  username varchar(255) NOT NULL UNIQUE, 
  password varchar(255) NOT NULL, 
  email    varchar(255) NOT NULL UNIQUE, 
  phone    varchar(20) NOT NULL, 
  fullname nvarchar(255) NOT NULL, 
  gender   bit NOT NULL, 
  is_super bit NOT NULL, 
  PRIMARY KEY (id));
CREATE TABLE user_class (
  classid  int NOT NULL, 
  userid   int NOT NULL, 
  courseid int NOT NULL, 
  PRIMARY KEY (classid, 
  userid, 
  courseid));
CREATE TABLE user_group (
  groupid int NOT NULL, 
  userid  int NOT NULL, 
  PRIMARY KEY (groupid, 
  userid));
CREATE INDEX class_id 
  ON class (id);
CREATE INDEX course_id 
  ON course (id);
CREATE INDEX group_id 
  ON [group] (id);
CREATE INDEX room_id 
  ON room (id);
CREATE INDEX slot_id 
  ON slot (id);
CREATE INDEX user_id 
  ON [user] (id);
ALTER TABLE [session] ADD CONSTRAINT class_session FOREIGN KEY (classid) REFERENCES class (id);
ALTER TABLE [session] ADD CONSTRAINT couse_session FOREIGN KEY (courseid) REFERENCES course (id);
ALTER TABLE user_group ADD CONSTRAINT group_user_group FOREIGN KEY (groupid) REFERENCES [group] (id);
ALTER TABLE [session] ADD CONSTRAINT room_session FOREIGN KEY (roomid) REFERENCES room (id);
ALTER TABLE attended ADD CONSTRAINT session_attended FOREIGN KEY (roomid, slotid, [date]) REFERENCES [session] (roomid, slotid, [date]);
ALTER TABLE [session] ADD CONSTRAINT slot_session FOREIGN KEY (slotid) REFERENCES slot (id);
ALTER TABLE attended ADD CONSTRAINT user_attended FOREIGN KEY (userid) REFERENCES [user] (id);
ALTER TABLE user_class ADD CONSTRAINT user_class_class FOREIGN KEY (classid) REFERENCES class (id);
ALTER TABLE user_class ADD CONSTRAINT user_class_course FOREIGN KEY (courseid) REFERENCES course (id);
ALTER TABLE [session] ADD CONSTRAINT user_session_teacher FOREIGN KEY (userid) REFERENCES [user] (id);
ALTER TABLE user_group ADD CONSTRAINT user_user_group FOREIGN KEY (userid) REFERENCES [user] (id);
ALTER TABLE user_class ADD CONSTRAINT user_user_room FOREIGN KEY (userid) REFERENCES [user] (id);
