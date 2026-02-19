CREATE TABLE IF NOT EXISTS clothes (
    id serial NOT NULL UNIQUE PRIMARY KEY ,
    name varchar(30) NOT NULL,
    description varchar(75)
);

CREATE TABLE IF NOT EXISTS clothes_list (
    id serial NOT NULL UNIQUE PRIMARY KEY,
    name varchar(30),
    created_at timestamp NOT NULL DEFAULT now()
);

CREATE TABLE IF NOT EXISTS clothes_list_elements
(
    id_clothes_list serial NOT NULL REFERENCES clothes_list (id) ON DELETE CASCADE,
    id_clothes      serial NOT NULL REFERENCES clothes (id) ON DELETE CASCADE,
    quantity        int    NOT NULL DEFAULT 0,
    CONSTRAINT unique_clothes_list_element UNIQUE (id_clothes_list, id_clothes)
)