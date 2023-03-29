# Simple API with MySQL


Example of request body for creating **Position**.

Validations were added for grade and empty name field
```json
{
   "name": "director",
   "grade": 1
}
```

Example of request body to update existing **Position**.

Fields can be empty besides id field. Updating only not empty fields

Update only name
```json
{
   "id": 1,
   "name": "main director"
}
```
Update only grade
```json
{
   "id": 1,
   "grade": 5
}
```

---

Example of request body for creating **Employee**.

Validations were added for empy Birthday and FIO fields. Position id list can be empty
```json
{
  "fio": "John Doe",
  "birthday": "1990-03-29T20:27:01.615Z",
  "positions": [
    1, 2
  ]
}
```

Example of request body to update existing **Employee**.

Fields can be empty besides id field. Updating only not empty fields

Update Birthday and Positions
```json
{
  "id": 1,
  "birthday": "1990-03-29T20:27:01.615Z",
  "positions": [
    1
  ]
}
```
Update only FIO
```json
{
  "id": 1,
  "fio": "John Boe"
}
```
Update only positions
```json
{
  "id": 1,
  "positions": [
    1, 3
  ]
}
```
