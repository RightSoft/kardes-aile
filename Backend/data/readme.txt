The data comes from:
https://github.com/dr5hn/countries-states-cities-database

Used states.csv

Import states.csv into states table.

They can be refilled with:
Add a original_id <int> column in country table.
Then:
insert into kardesaile.country (SELECT gen_random_uuid(), country_name, timezone('UTC'::text, now()), 'admin', country_id, country_code from states s
group by country_id, country_code, country_name);

insert into kardesaile.city (SELECT gen_random_uuid(), name, timezone('UTC'::text, now()), 'admin', country.id, city.state_code from states city, (
select id, original_id from kardesaile.country) country
where country.original_id = city.country_id);
