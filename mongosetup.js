
conn = new Mongo();
db = conn.getDB("admin");

db.createUser( { user: "sw_test_user",  pwd: "testing", roles: [ { role: "readWrite", db: "sidwatch_test" } ] } )