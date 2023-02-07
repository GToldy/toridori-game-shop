INSERT INTO Supplier (name, description) VALUES ('Amazon', 'Digital content and services');
INSERT INTO Supplier (name, description) VALUES ('Hasbro', 'Provides games to all ages');
INSERT INTO Supplier (name, description) VALUES ('Steam', 'Wide variety of games can be found');
INSERT INTO Supplier (name, description) VALUES ('Ubisoft', 'Wide variety of games can be found');

INSERT INTO ProductCategory (name, description) VALUES ('Adult', 'A game played by adlt players');
INSERT INTO ProductCategory (name, description) VALUES ('Card', 'A game mainly or only played using cards');
INSERT INTO ProductCategory (name, description) VALUES ('Board', 'A game including a board or mat, and moving parts');
INSERT INTO ProductCategory (name, description) VALUES ('PC Game', 'Game software');

INSERT INTO Product (name, description, players, currency, default_price, product_category_id, supplier_id, image)
	VALUES ('Cluedo', 'A mistery game', '3-6', 'USD', 23.5, 2, 1, 'Cluedo.jpg');
INSERT INTO Product (name, description, players, currency, default_price, product_category_id, supplier_id, image)
	VALUES ('Codenames', 'A mistery game', '3-6', 'USD', 23.5, 1, 1, 'Codenames.jpg');
INSERT INTO Product (name, description, players, currency, default_price, product_category_id, supplier_id, image)
	VALUES ('Phasmophobia', '4 player online co-op psychological horror. Paranormal activity is on the rise and it’s up to you and your team to use all the ghost hunting equipment at your disposal in order to gather as much evidence as you can.', '1-4', 'USD', 23.5, 3, 2, 'Phasmophobia.jpg');
INSERT INTO Product (name, description, players, currency, default_price, product_category_id, supplier_id, image)
	VALUES ('Foreplay in a row', 'An adult twist on a fun little game.', '2-?', 'USD', 23.5, 1, 1, 'Foreplay in a row.jpg');
INSERT INTO Product (name, description, players, currency, default_price, product_category_id, supplier_id, image)
	VALUES ('Tom Clancy''s Rainbow Six Siege', 'Tom Clancy''s Rainbow Six Siege is the latest installment of the acclaimed first-person shooter franchise developed by the renowned Ubisoft Montreal studio.', '1-10', 'USD', 65.5, 3, 3, 'Siege.jpg');
INSERT INTO Product (name, description, players, currency, default_price, product_category_id, supplier_id, image)
	VALUES ('Civilisations IV', 'Turn-based strategy games, a genre in which players control an empire and explore (expand, exploit, and exterminate), by having the player attempt to lead a modest group of peoples from a base with initially scarce resources into a successful empire or civilization.', '1', 'USD', 89.0, 3, 2, 'Civilization.jpg');