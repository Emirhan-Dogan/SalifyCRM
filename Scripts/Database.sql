CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    first_name VARCHAR(255) NOT NULL,
    last_name VARCHAR(255) NOT NULL,
    citizen_id VARCHAR(20) NOT NULL UNIQUE,
    phone_number VARCHAR(20) NOT NULL,
    email VARCHAR(255) NOT NULL UNIQUE,
    password_salt VARCHAR(255) NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    last_login_date TIMESTAMP,
    gender VARCHAR(10),
    birth_date DATE,
    profile_image TEXT,
    notes TEXT,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status BOOLEAN,
    last_updated_user_id INT,
    last_updated_date TIMESTAMP
);

CREATE TABLE operation_claims (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    is_deleted BOOLEAN DEFAULT FALSE
);

CREATE TABLE user_operation_claims (
    id SERIAL PRIMARY KEY,
    user_id INT NOT NULL,
    operation_claim_id INT NOT NULL,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP,
    CONSTRAINT fk_user FOREIGN KEY(user_id) REFERENCES users(id) ON DELETE CASCADE,
    CONSTRAINT fk_operation_claim FOREIGN KEY(operation_claim_id) REFERENCES operation_claims(id) ON DELETE CASCADE
);

CREATE TABLE groups (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP
);

CREATE TABLE group_claims (
    id SERIAL PRIMARY KEY,
    operation_claim_id INT NOT NULL,
    group_id INT NOT NULL,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP,
    CONSTRAINT fk_operation_claim FOREIGN KEY(operation_claim_id) REFERENCES operation_claims(id) ON DELETE CASCADE,
    CONSTRAINT fk_group FOREIGN KEY(group_id) REFERENCES groups(id) ON DELETE CASCADE
);

CREATE TABLE user_groups (
    id SERIAL PRIMARY KEY,
    user_id INT NOT NULL,
    group_id INT NOT NULL,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP,
    CONSTRAINT fk_user FOREIGN KEY(user_id) REFERENCES users(id) ON DELETE CASCADE,
    CONSTRAINT fk_group FOREIGN KEY(group_id) REFERENCES groups(id) ON DELETE CASCADE
);


-- Customer Management

create table customer_types(
id SERIAL primary key,
name VARCHAR(50) not null,
descriptions varchar(300) null,
is_deleted BOOLEAN DEFAULT FALSE,
created_user_id INT NOT NULL,
created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
status VARCHAR(50),
last_updated_user_id INT,
last_updated_date TIMESTAMP
);


create table customer_tags(
id SERIAL primary key,
name VARCHAR(50) not null,
descriptions varchar(300) null,
is_deleted BOOLEAN DEFAULT FALSE,
created_user_id INT NOT NULL,
created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
status VARCHAR(50),
last_updated_user_id INT,
last_updated_date TIMESTAMP
);

create table occupations(
id SERIAL primary key,
name VARCHAR(50) not null,
descriptions varchar(300) null,
is_deleted BOOLEAN DEFAULT FALSE,
created_user_id INT NOT NULL,
created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
status VARCHAR(50),
last_updated_user_id INT,
last_updated_date TIMESTAMP
);

create table martial_statuses(
id SERIAL primary key,
name VARCHAR(50) not null,
descriptions varchar(300) null,
is_deleted BOOLEAN DEFAULT FALSE,
created_user_id INT NOT NULL,
created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
status VARCHAR(50),
last_updated_user_id INT,
last_updated_date TIMESTAMP
);

create table countries(
id serial primary key,
name varchar(50) not null,
is_deleted BOOLEAN DEFAULT FALSE,
created_user_id INT NOT NULL,
created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
status VARCHAR(50),
last_updated_user_id INT,
last_updated_date TIMESTAMP
);

create table cities(
id serial primary key,
name varchar(50) not null,
country_id int not null,

is_deleted BOOLEAN DEFAULT FALSE,
created_user_id INT NOT NULL,
created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
status VARCHAR(50),
last_updated_user_id INT,
last_updated_date TIMESTAMP,

constraint fk_country foreign key(country_id) references countries(id) on delete cascade
);

create table districts(
id serial primary key,
name varchar(50) not null,
city_id int not null,

is_deleted BOOLEAN DEFAULT FALSE,
created_user_id INT NOT NULL,
created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
status VARCHAR(50),
last_updated_user_id INT,
last_updated_date TIMESTAMP,

constraint fk_city foreign key(city_id) references cities(id) on delete cascade
);

create table address_types(
id serial primary key,
name varchar(50) not null,
descriptions varchar(300) null,

is_deleted BOOLEAN DEFAULT FALSE,
created_user_id INT NOT NULL,
created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
status VARCHAR(50),
last_updated_user_id INT,
last_updated_date TIMESTAMP
);

create table addresses(
id serial primary key,
name varchar(50) not null,
details varchar(500) null,

address_type_id int not null,
country_id int not null,
city_id int not null,
district_id int not null,

is_deleted BOOLEAN DEFAULT FALSE,
created_user_id INT NOT NULL,
created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
status VARCHAR(50),
last_updated_user_id INT,
last_updated_date TIMESTAMP,

constraint fk_address_type foreign key(address_type_id) references address_types(id) on delete cascade,
constraint fk_country foreign key(country_id) references countries(id) on delete cascade,
constraint fk_cities foreign key(city_id) references cities(id) on delete cascade,
constraint fk_district foreign key(district_id) references districts(id) on delete cascade
);

create table customers(
id serial primary key,
customer_code varchar(15) not null,
first_name varchar(50) not null,
last_name varchar(50) not null,
email varchar(100) not null,
phone_number varchar(15),
birth_date timestamp,
gender boolean,
income_level int,
last_login_date timestamp default null,
last_interaction_date timestamp default null,

customer_type_id int not null,
occupation_id int,
martial_status_id int,
customer_category_id int not null,

is_deleted BOOLEAN DEFAULT FALSE,
created_user_id INT NOT NULL,
created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
status VARCHAR(50),
last_updated_user_id INT,
last_updated_date TIMESTAMP,

constraint fk_customer_type foreign key(customer_type_id) references customer_types(id) on delete cascade,
constraint fk_occupation foreign key(occupation_id) references occupations(id) on delete cascade,
constraint fk_martial_status foreign key(martial_status_id) references martial_statuses(id) on delete cascade,
constraint fk_customer_category foreign key(customer_category_id) references customer_category(id) on delete cascade
);

create table customer_notes(
id serial primary key,
note varchar(300) not null,
note_date timestamp default CURRENT_TIMESTAMP,
customer_id int not null,

is_deleted BOOLEAN DEFAULT FALSE,
created_user_id INT NOT NULL,
created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
status VARCHAR(50),
last_updated_user_id INT,
last_updated_date TIMESTAMP,

constraint fk_customer foreign key(customer_id) references customers(id) on delete cascade
);

create table customer_tag_mappings(
id serial primary key,
customer_id int not null,
customer_tag_id int not null,

is_deleted BOOLEAN DEFAULT FALSE,
created_user_id INT NOT NULL,
created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
status VARCHAR(50),
last_updated_user_id INT,
last_updated_date TIMESTAMP,

constraint fk_customer foreign key(customer_id) references customers(id) on delete cascade,
constraint fk_customer_tag foreign key(customer_tag_id) references customer_tags(id) on delete cascade
);

create  table customer_addresses(
id serial primary key,
customer_id int not null,
address_id int not null,

is_deleted BOOLEAN DEFAULT FALSE,
created_user_id INT NOT NULL,
created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
status VARCHAR(50),
last_updated_user_id INT,
last_updated_date TIMESTAMP,

constraint fk_customer foreign key(customer_id) references customers(id) on delete cascade,
constraint fk_address foreign key(address_id) references addresses(id) on delete cascade
);

create table permissions(
id serial primary key,
name varchar(50) not null,
document_path varchar(500) not null,
descriptions varchar(500),

is_deleted BOOLEAN DEFAULT FALSE,
created_user_id INT NOT NULL,
created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
status VARCHAR(50),
last_updated_user_id INT,
last_updated_date TIMESTAMP
);
create table social_medias(
id serial primary key,
name varchar(50) not null,
icon varchar(500) not null,

is_deleted BOOLEAN DEFAULT FALSE,
created_user_id INT NOT NULL,
created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
status VARCHAR(50),
last_updated_user_id INT,
last_updated_date TIMESTAMP
);
create table customer_permissions(
id serial primary key,
customer_id int not null,
permission_id int not null,

is_deleted BOOLEAN DEFAULT FALSE,
created_user_id INT NOT NULL,
created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
status VARCHAR(50),
last_updated_user_id INT,
last_updated_date TIMESTAMP,
constraint fk_customer foreign key(customer_id) references customers(id) on delete cascade,
constraint fk_permission foreign key(permission_id) references permissions(id) on delete cascade
);

create table customer_socials(
id serial primary key,
customer_id int not null,
social_media_id int not null,
social_media_link varchar(500) not null,

is_deleted BOOLEAN DEFAULT FALSE,
created_user_id INT NOT NULL,
created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
status VARCHAR(50),
last_updated_user_id INT,
last_updated_date TIMESTAMP,

constraint fk_customer foreign key(customer_id) references customers(id) on delete cascade,
constraint fk_social_medias foreign key(social_media_id) references social_medias(id) on delete cascade
);

create table customer_categories(
id serial primary key,
name varchar(70) not null,
descriptions varchar(300),
is_deleted BOOLEAN DEFAULT FALSE,
created_user_id INT NOT NULL,
created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
status VARCHAR(50),
last_updated_user_id INT,
last_updated_date TIMESTAMP
);


-- Create Admin User
INSERT INTO public.users
( first_name, last_name, citizen_id, phone_number, email, password_salt, password_hash, last_login_date, gender, birth_date, profile_image, notes, is_deleted, created_user_id, created_date, status, last_updated_user_id, last_updated_date)
VALUES('Admin', 'Administrator', '', '00', 'emirhandoganresmi@gmail.com', '4jBGAN9DUd5Sf9Y08T1j/5wNM+e2CvxARfcq6zDgvsiemwYHTh6QIg/Qf31cscBS8ajLXmVbdPjFAtxCG6Lf+Q==', 'VVUn7A6IQKMtzqz3pQa4ExKD9+93Z2Y++015sPRckcE=', CURRENT_TIMESTAMP, 'E', CURRENT_TIMESTAMP, '', '', false, 0, CURRENT_TIMESTAMP, true, 0, CURRENT_TIMESTAMP);

-- Product
create table product_categories(
    id serial primary key,
    parrent_category_id INT null,
    name varchar(70) not null,
    descriptions varchar(300),
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP,
    constraint fk_categories foreign key(parrent_category_id) references product_categories(id) on delete cascade
);

create table colors(
    id serial primary key,
    name varchar(70) not null,
    color_code varchar(7) not null,
    descriptions varchar(300),
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP
);

create table materials(
    id serial primary key,
    name varchar(70) not null,
    material_code varchar(15) not null,
    descriptions varchar(300),
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP
);

create table product_tags(
    id serial primary key,
    name varchar(70) not null,
    descriptions varchar(300),
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP
);

create table store_types(
    id serial primary key,
    name varchar(70) not null,
    descriptions varchar(300),
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP
);

create table stores(
    id serial primary key,
    address_id INT not null,
    store_type_id INT not null,
    name varchar(70) not null,
    store_code varchar(15) not null,
    capacity INT null, 
    descriptions varchar(300),
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP,
    
    constraint fk_stores_address foreign key(address_id) references addresses(id) on delete cascade,
    constraint fk_stores_store_type foreign key(store_type_id) references store_types(id) on delete cascade
);

CREATE TABLE products (
    id SERIAL PRIMARY key,
    store_id INT NOT null,
    product_code VARCHAR(50) NOT null,
    barcode_number VARCHAR(50) UNIQUE NOT null,
    name VARCHAR(255) NOT null,
    description text,
    unit_in_stock INT NOT NULL DEFAULT 0,
    is_subscription BOOLEAN NOT NULL DEFAULT false,
    price NUMERIC(10, 2) NOT null,
    weight NUMERIC(10, 3),
    length NUMERIC(10, 2),
    width NUMERIC(10, 2),
    height NUMERIC(10, 2),
    volume NUMERIC(12, 4),
    minimum_stock_count INT NOT NULL DEFAULT 0,
    tax_rate NUMERIC(5, 2) NOT NULL DEFAULT 0.00,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP,
    constraint fk_products_store foreign key(store_id) references stores(id) on delete cascade
);

create table subscriptions(
    id serial primary key,
    product_id INT not null,
    name varchar(70) not null,
    descriptions varchar(300),
    period INT not null,
    price numeric not null,
    end_date TIMESTAMP null,
    start_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP,
    constraint fk_subscriptions_product foreign key(product_id) references products(id) on delete cascade
);

create table product_notes(
    id serial primary key,
    product_id INT not null,
    note varchar(500) not null,
    note_date TIMESTAMP default CURRENT_TIMESTAMP,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP,
    constraint fk_product_notes foreign key(product_id) references products(id) on delete cascade
);

create table product_images(
    id serial primary key,
    product_id INT not null,
    image_path varchar(500) not null,
    is_highlights BOOLEAN default false,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP,
    constraint fk_product_images foreign key(product_id) references products(id) on delete cascade
);

create table product_category_mappings(
    id serial primary key,
    product_id INT not null,
    product_category_id int not null,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP,
    constraint fk_product_category_mappings_product foreign key(product_id) references products(id) on delete cascade,
    constraint fk_product_category_mappings_category foreign key(product_category_id) references product_categories(id) on delete cascade
);

create table product_colors(
    id serial primary key,
    product_id INT not null,
    color_id int not null,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP,
    constraint fk_product_colors foreign key(product_id) references products(id) on delete cascade,
    constraint fk_color foreign key(color_id) references colors(id) on delete cascade
);

create table product_material(
    id serial primary key,
    product_id INT not null,
    material_id int not null,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP,
    constraint fk_product_material foreign key(product_id) references products(id),
    constraint fk_material foreign key(material_id) references materials(id)
);

create table product_tag_mappings(
    id serial primary key,
    product_id INT not null,
    product_tag_id int not null,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP,
    constraint fk_product_tag_mappings_product foreign key(product_id) references products(id) on delete cascade,
    constraint fk_product_tag_mappings_tag foreign key(product_tag_id) references product_tags(id) on delete cascade
);

-- Order
create table order_statuses(
    id serial primary key,
    name varchar(70) not null,
    descriptions varchar(300),
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP
);

create table orders(
    id serial primary key,
    order_status_id int not null,
    customer_id int not null,
    customer_email varchar(70) not null,
    customer_phone_number varchar(15) null,
    total_price NUMERIC(10, 2) NOT null,
    address_id int null,
    address_country_id int null,
    address_city_id int null,
    address_district_id int null,
    address_detail varchar(700) null,
    order_date timestamp default CURRENT_TIMESTAMP,
    is_subscription BOOLEAN NOT NULL DEFAULT false,
    notes varchar(700) null,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP,
    
    constraint fk_orders_address foreign key(address_id) references addresses(id) on delete cascade,
    constraint fk_orders_status foreign key(order_status_id) references order_statuses(id) on delete cascade
);

create table order_products(
    id serial primary key,
    order_id int not null,
    product_id int not null,
    piece int not null,
    unit_in_price NUMERIC(10, 2) NOT null,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP,
    
    constraint fk_order_products_order foreign key(order_id) references orders(id) on delete cascade,
    constraint fk_order_products_product foreign key(product_id) references products(id) on delete cascade
);

create table order_product_details(
    id serial primary key,
    order_product_id int not null,
    name varchar(50) not null,
    value varchar(500) not null,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP,
    
    constraint fk_order_product_details foreign key(order_product_id) references order_products(id) on delete cascade
);

create table order_subscriptions(
    id serial primary key,
    order_id int not null,
    price NUMERIC(10, 2) NOT null,
    start_date timestamp default CURRENT_TIMESTAMP,
    end_date timestamp NULL,
    is_active boolean default false,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP,
    
    constraint fk_order_subscriptions_order foreign key(order_id) references orders(id) on delete cascade
);

-- Activity
create table activity_types(
    id serial primary key,
    name varchar(70) not null,
    descriptions varchar(300),
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP
);

create table activity_statuses(
    id serial primary key,
    name varchar(70) not null,
    descriptions varchar(300),
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP
);

create table activities(
    id serial primary key,
    name varchar(70) not null,
    location varchar(300) not null,
    descriptions varchar(300),
    customer_id int not null,
    activity_type_id int not null,
    activity_status_id int not null,
    start_date TIMESTAMP default CURRENT_TIMESTAMP,
    end_date TIMESTAMP default CURRENT_TIMESTAMP,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP,
    
    constraint fk_customer foreign key(customer_id) references customers(id) on delete cascade,
    constraint fk_activities_type foreign key(activity_type_id) references activity_types(id) on delete cascade,
    constraint fk_activities_status foreign key(activity_status_id) references activity_statuses(id) on delete cascade
);

create table activity_notes(
    id serial primary key,
    activity_id int not null,
    note varchar(500) not null,
    note_date TIMESTAMP default CURRENT_TIMESTAMP,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP,
    
    constraint fk_activity_notes foreign key(activity_id) references activities(id) on delete cascade
);

create table activity_users(
    id serial primary key,
    email varchar(70) not null,
    activity_id int not null,
    user_id int not null,
    is_verify_participation boolean default false,
    activity_participation_date TIMESTAMP null,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP,
    
    constraint fk_activity_users_activity foreign key(activity_id) references activities(id) on delete cascade
);


-- Interaction
create table interaction_types(
    id serial primary key,
    name varchar(70) not null,
    descriptions varchar(300),
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP
);

create table interactions(
    id serial primary key,
    interaction_type_id int not null,
    customer_id int not null,
    order_id int null,
    order_code varchar(50) null,
    interaction_date TIMESTAMP default CURRENT_TIMESTAMP,
    descriptions varchar(300),
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP,
    
    constraint fk_interactions_type foreign key(interaction_type_id) references interaction_types(id) on delete cascade,
    constraint fk_interactions_order foreign key(order_id) references orders(id) on delete cascade
);

create table interaction_notes(
    id serial primary key,
    interaction_id int not null,
    note varchar(500) not null,
    note_date TIMESTAMP default CURRENT_TIMESTAMP,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_user_id INT NOT NULL,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50),
    last_updated_user_id INT,
    last_updated_date TIMESTAMP,
    
    constraint fk_interaction_notes foreign key(interaction_id) references interactions(id) on delete cascade
);



