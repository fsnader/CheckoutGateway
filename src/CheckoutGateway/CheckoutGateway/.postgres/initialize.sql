create extension if not exists "uuid-ossp";

CREATE TABLE merchant
(
    id        uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
    name      text NOT NULL UNIQUE,
    client_id text NOT NULL,
    secret_id text NOT NULL
);

-- Enum: payment_status
CREATE TYPE payment_status AS ENUM (
    'Created',
    'Declined',
    'Processed'
);

-- Table: payment
CREATE TABLE payment
(
    id                    uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
    merchant_id           uuid NOT NULL,
    bank_external_id      uuid,
    amount                numeric NOT NULL,
    currency              text NOT NULL,
    status                payment_status NOT NULL,
    card_name             text NOT NULL,
    card_number           text NOT NULL,
    card_scheme           text  NOT NULL,
    card_expiration_month integer NOT NULL,
    card_expiration_year  integer NOT NULL,
    card_cvv              integer NOT NULL,
    created_at            timestamp WITH TIME ZONE,
    updated_at            timestamp WITH TIME ZONE,
    CONSTRAINT fk_payment_merchant FOREIGN KEY (merchant_id) REFERENCES merchant (id)
);





