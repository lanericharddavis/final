CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaults(
  id INT AUTO_INCREMENT NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId VARCHAR(255) NOT NULL COMMENT 'FK: User Account',
  name varchar(255) NOT NULL COMMENT 'Vault Name',
  description VARCHAR(255) COMMENT 'Vault Description',
  isPrivate TINYINT COMMENT 'Vault Privacy',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS keeps(
  id INT AUTO_INCREMENT NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId VARCHAR(255) NOT NULL COMMENT 'FK: User Account',
  name varchar(255) NOT NULL COMMENT 'Keep Name',
  description VARCHAR(255) COMMENT 'Keep Description',
  img VARCHAR(255) COMMENT 'Keep Description',
  views int NOT NULL COMMENT 'Keep Views',
  shares int NOT NULL COMMENT 'Keep Shares',
  keeps int NOT NULL COMMENT 'Keep Keeps',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaultkeeps(
  id INT AUTO_INCREMENT NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId varchar(255) NOT NULL COMMENT 'FK: User Account',
  vaultId int NOT NULL COMMENT 'FK: Vault',
  keepId int NOT NULL COMMENT 'FK: Keep',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
INSERT INTO
  vaultkeeps(creatorId, vaultId, keepId)
VALUES
  (
    "cd5f28ba-d192-4c30-9434-b39c95fdfdfa",
    2,
    2
  );
SELECT
  k.*,
  v.*,
  vk.id as vaultKeepId,
  vk.vaultId as vaultId,
  vk.keepId as keepId
FROM
  vaultkeeps vk
  JOIN keeps k ON k.id = vk.keepId
  JOIN vaults v ON v.id = vk.vaultId
WHERE
  vk.Id = @2;
SELECT
  k.*,
  v.*,
  vk.id as vaultKeepId,
  vk.vaultId as vaultId,
  vk.keepId as keepId
FROM
  vaultkeeps vk
  JOIN keeps k ON k.id = vk.keepId
  JOIN vaults v ON v.id = vk.vaultId
WHERE
  vk.vaultId = 1;
INSERT INTO
  vaults(name, description, isPrivate, creatorId)
VALUES
  (
    "Puppies",
    "Look at all the cute puppies!",
    false,
    "cd5f28ba-d192-4c30-9434-b39c95fdfdfa"
  );
INSERT INTO
  keeps(
    name,
    description,
    img,
    views,
    shares,
    keeps,
    creatorId
  )
VALUES
  (
    "Lovely Labs",
    "Chocolate is good",
    "//placehold.it/500x500",
    0,
    0,
    0,
    "cd5f28ba-d192-4c30-9434-b39c95fdfdfa"
  );
INSERT INTO
  vaults(name, description, isPrivate, creatorId)
VALUES
  (
    "Kittens!",
    "Look at all the cute kittens!",
    false,
    "81e3908a-3997-4f25-bb36-b8d38060cde4"
  );
INSERT INTO
  keeps(
    name,
    description,
    img,
    views,
    shares,
    keeps,
    creatorId
  )
VALUES
  (
    "Fluffy",
    "The fluffier, the better!",
    "//placehold.it/500x500",
    0,
    0,
    0,
    "81e3908a-3997-4f25-bb36-b8d38060cde4"
  );
  /* SELECT
              g.*,
              g.id AS groupId,
              a.name as creatorName,
              a.picture as creatorPic
            FROM
              groups g
              JOIN accounts a ON a.id = g.creatorId
            WHERE
              g.id = 3; */
  /* SELECT
              c.*,
              g.*,
              a.name,
              a.picture
            FROM
              comments c
              JOIN accounts a ON c.creatorId = a.id;
              JOIN groups g ON c.groupId = g.id;
            WHERE
              groupId = 3; */