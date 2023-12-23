
db = db.getSiblingDB('admin');

db.auth("root","password")
db = db.getSiblingDB('StepMaster');

db.createCollection('Regions');
db.createCollection('Days');
db.createCollection('Rating');
db.createCollection('User');
db.Rating.insertMany(
  [
    {
      "date": {
        "$date": "2023-12-12T09:18:09.309Z"
      },
      "regionId": "test",
      "ratingUsers": []
    }    
  ]
)
db.Days.insertMany(
  [
    {"calories": 100.1,
  "distance": 10.1,
  "steps": 10000,
  "plancalories": 300.1,
  "plandistance": 15.2,
  "plansteps": 15000,
  "date": "21/12/2023",
  "email": "1234@gmail.com"
} ]
)
db.User.insertMany(
  [{   
    "email": "paxar14705@notedns.com",
    "nickname": "Da?sdfsdf",
    "fullname": "Net?",
    "role": "user",
    "rating": {
      "placeInRegion": "1/1",
      "placeInCountry": "3/3"
    },
    "password": "d5KDX1satAqslCkx8k4tFw==;S069ToBh8HO0g93ZypHRT/65BC2AcFlioJKjffQjAL4=",
    "region_id": "6546398a763aafa12565285e",
    "gender": "male",
    "lastCookie": null
}
]);
  
  db.Regions.insertMany([    
    {"fullName": "Республика Адыгея"},
    { "fullName": "Республика Карелия" },
    { "fullName": "Республика Коми" },
    { "fullName": "Республика Марий Эл" },
    { "fullName": "Республика Мордовия" },
    { "fullName": "Республика Саха Якутия" },
    { "fullName": "Республика Северная Осетия - Алания" },
    { "fullName": "Республика Татарстан" },
    { "fullName": "Республика Тыва" },
    { "fullName": "Республика Удмуртская" },
    { "fullName": "Республика Хакасия" },
    { "fullName": "Республика Башкортостан" },
    { "fullName": "Республика Чеченская" },
    { "fullName": "Чувашская Республика - Чувашия" },
    { "fullName": "Алтайский Край" },
    { "fullName": "Краснодарский Край" },
    { "fullName": "Красноярский Край" },
    { "fullName": "Приморский Край" },
    { "fullName": "Ставропольский Край" },
    { "fullName": "Хабаровский Край" },
    { "fullName": "Амурская Область" },
    { "fullName": "Архангельская Область" },
    { "fullName": "Республика Бурятия" },
    { "fullName": "Астраханская Область" },
    { "fullName": "Белгородская Область" },
    { "fullName": "Брянская Область" },
    { "fullName": "Владимирская Область" },
    { "fullName": "Волгоградская Область" },
    { "fullName": "Вологодская Область" },
    { "fullName": "Воронежская Область" },
    { "fullName": "Ивановская Область" },
    { "fullName": "Иркутская Область" },
    { "fullName": "Калининградская Область" },
    { "fullName": "Республика Алтай" },
    { "fullName": "Калужская Область" },{
       "fullName": "Камчатский Край" },
       { "fullName": "Кемеровская область - Кузбасс Область" },
       { "fullName": "Кировская Область" },
       { "fullName": "Костромская Область" },
       { "fullName": "Курганская Область" },
       { "fullName": "Курская Область" },
       { "fullName": "Ленинградская Область" },
       { "fullName": "Липецкая Область" },
       { "fullName": "Магаданская Область" },
       { "fullName": "Республика Дагестан" },
       { "fullName": "Московская Область" },
       { "fullName": "Мурманская Область" },
       { "fullName": "Нижегородская Область" },
       { "fullName": "Новгородская Область" },
       { "fullName": "Новосибирская Область" },
       { "fullName": "Омская Область" },
       { "fullName": "Оренбургская Область" },
       { "fullName": "Орловская Область" },
       { "fullName": "Пензенская Область" },
       {"fullName": "Пермский Край" },
       { "fullName": "Республика Ингушетия" },
       { "fullName": "Псковская Область" },
       { "fullName": "Ростовская Область" },
       { "fullName": "Рязанская Область" },
       { "fullName": "Самарская Область" },
       { "fullName": "Саратовская Область" },
       { "fullName": "Сахалинская Область" },
       { "fullName": "Свердловская Область" },
       { "fullName": "Смоленская Область" },
       { "fullName": "Тамбовская Область" },
       { "fullName": "Тверская Область" },
       { "fullName": "Республика Кабардино-Балкарская" },
       { "fullName": "Томская Область" },
       { "fullName": "Тульская Область" },
       { "fullName": "Тюменская Область" },
       { "fullName": "Ульяновская Область" },
       { "fullName": "Челябинская Область" },
       { "fullName": "Забайкальский Край" },
       { "fullName": "Ярославская Область" },
       { "fullName": "Москва Город" },
       { "fullName": "Санкт-Петербург Город" },
       { "fullName": "Еврейская Автономная область" },
       { "fullName": "Республика Калмыкия" },
       { "fullName": "Ненецкий Автономный округ" },
       { "fullName": "Ханты-Мансийский Автономный округ - Югра Автономный округ" },
       { "fullName": "Чукотский Автономный округ" },
       { "fullName": "Ямало-Ненецкий Автономный округ" },
       { "fullName": "Республика Карачаево-Черкесская" },
       { "fullName": "Запорожская Область" },
       { "fullName": "Республика Крым" },
       { "fullName": "Севастополь Город" },
       { "fullName": "Республика Донецкая Народная" },
       { "fullName": "Республика Луганская Народная" },
       { "fullName": "Херсонская Область" },
       { "fullName": "Байконур Город" } ]   
    );
  



