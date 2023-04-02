CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

ALTER TABLE accounts 
ADD COLUMN coverImg VARCHAR(5000);

CREATE TABLE IF NOT EXISTS Keep(
id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
creatorId VARCHAR(255) NOT NULL,
name VARCHAR(255) NOT NULL,
description VARCHAR(255) NOT NULL,
img VARCHAR(255) NOT NULL,
views INT NOT NULL,

FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

ALTER TABLE Keep
ADD COLUMN kept INT NOT NULL;


INSERT INTO Keep
(creatorId, name, description, img, views)
VALUES
("641ca377ca6451d6285435cb", "big house", "beautiful house!", "https://images.unsplash.com/photo-1558036117-15d82a90b9b1?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Nnx8YmVhdXRpZnVsJTIwaG9tZXN8ZW58MHx8MHx8&auto=format&fit=crop&w=800&q=60", 5 );

CREATE TABLE IF NOT EXISTS Vault(
id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
creatorId VARCHAR(255) NOT NULL,
name VARCHAR(255) NOT NULL,
description VARCHAR(255) NOT NULL,
img VARCHAR(2555) NOT NULL,
isPrivate BOOLEAN NOT NULL DEFAULT FALSE,
Foreign Key (creatorId) REFERENCES accounts(id) ON DELETE CASCADE

) default charset utf8 COMMENT '';



DROP TABLE Vault;


INSERT INTO Vault
(creatorId, name, description, img)
VALUES
("641ca377ca6451d6285435cb", "manifestations", "attract amazing things", "https://images.unsplash.com/photo-1506905925346-21bda4d32df4?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80");



CREATE TABLE IF NOT EXISTS VaultKeep(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
creatorId VARCHAR(255) NOT NULL,
vaultId INT NOT NULL,
keepId INT NOT NULL,
Foreign Key (vaultId) REFERENCES Vault(id) ON DELETE CASCADE,
Foreign Key (keepId) REFERENCES Keep(id) ON DELETE CASCADE,
Foreign Key (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';


INSERT INTO VaultKeep
(creatorId, vaultId, keepId)
VALUES
("641ca377ca6451d6285435cb", 1, 5);

SELECT
k.*,
COUNT(vk.id) AS kept
FROM Keep k 
LEFT JOIN VaultKeep vk ON k.id = vk.keepId
WHERE k.id = 62;

        SELECT
        k.*,
        COUNT(vk.id) AS kept,
        acct.*
        FROM Keep k 
        LEFT JOIN VaultKeep vk ON k.id = vk.keepId
        JOIN accounts acct ON k.creatorId = acct.id
        GROUP BY k.id;

                SELECT
        k.*,
        COUNT(vk.id) AS kept,
        acct.*
        FROM Keep k
        LEFT JOIN VaultKeep vk ON k.id = vk.keepId
        JOIN accounts acct ON k.creatorId = acct.id
        WHERE k.id = 63
        GROUP BY k.id;