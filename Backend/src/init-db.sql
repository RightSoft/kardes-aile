DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'kardesaile') THEN
        CREATE SCHEMA kardesaile;
    END IF;
END $EF$;
CREATE TABLE IF NOT EXISTS kardesaile.migrations_history (
    migration_id character varying(150) NOT NULL,
    product_version character varying(32) NOT NULL,
    CONSTRAINT pk_migrations_history PRIMARY KEY (migration_id)
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'kardesaile') THEN
        CREATE SCHEMA kardesaile;
    END IF;
END $EF$;

CREATE TABLE kardesaile."user" (
    id uuid NOT NULL,
    status integer NOT NULL,
    first_name character varying(100) NOT NULL,
    last_name character varying(100) NOT NULL,
    email character varying(255) NOT NULL,
    email_validated boolean NOT NULL,
    phone character varying(30) NOT NULL,
    phone_validated boolean NOT NULL,
    hash character varying(255) NULL,
    salt character varying(255) NULL,
    role integer NOT NULL,
    modified_by character varying(255) NULL,
    modified_at timestamp with time zone NULL,
    created_by character varying(255) NOT NULL,
    created_at timestamp with time zone NOT NULL,
    CONSTRAINT pk_user PRIMARY KEY (id)
);

CREATE TABLE kardesaile.cocuk (
    id uuid NOT NULL,
    ad character varying(50) NOT NULL,
    dogum_tarih timestamp with time zone NOT NULL,
    cinsiyet integer NOT NULL,
    user_id uuid NOT NULL,
    modified_by character varying(255) NULL,
    modified_at timestamp with time zone NULL,
    created_by character varying(255) NOT NULL,
    created_at timestamp with time zone NOT NULL,
    CONSTRAINT pk_cocuk PRIMARY KEY (id),
    CONSTRAINT fk_cocuk_user_user_id FOREIGN KEY (user_id) REFERENCES kardesaile."user" (id) ON DELETE CASCADE
);

CREATE INDEX ix_cocuk_user_id ON kardesaile.cocuk (user_id);

CREATE UNIQUE INDEX ix_user_email ON kardesaile."user" (email);

INSERT INTO kardesaile."user" (id, status, first_name, last_name, email, hash, salt, role, created_by, created_at, email_validated, phone_validated, phone)
VALUES ('483add12-29e2-423c-b0e0-17c8451772fb', 0, 'Test', 'Admin', 'user@example.com', 'Uyk+qILQLc2HI/mfKLsmvuqSwuNqlUvyzyO/66oz0OI=', '/swMcEv4n+B3e4pWAZv2YcLxP02tiiOq4ufwcbyBC/I=', 0, 'system', TIMESTAMPTZ '2023-02-11 12:33:09.526775Z', FALSE, FALSE, '123-345');

INSERT INTO kardesaile.migrations_history (migration_id, product_version)
VALUES ('20230211123204_InitialDatabase', '7.0.2');

COMMIT;
