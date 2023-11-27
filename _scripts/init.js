
db = db.getSiblingDB('admin');

db.auth("root","password")
db = db.getSiblingDB('StepMaster');

db.createCollection('Regions');
db.createCollection('Days');
db.createCollection('User')
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
},
{"calories": 100.1,
  "distance": 10.1,
  "steps": 10000,
  "plancalories": 300.1,
  "plandistance": 15.2,
  "plansteps": 15000,
  "date": "21/12/2023",
  "email": "1234@gmail.com"
}
  ]
)
db.User.insertMany(
  [{   
    "email": "dante_aligieri@rambler.ru",
    "nickname": "Ubivator_Zaharov_User",
    "fullname": "Ubivator_Zaharov_User",
    "role": "user",
    "password": "it1Cb/tDsMqGegBO2BVaqQ==;LFabWZeNkstVRdFm1zcUQ1+CIh48WpRobeNdAjDfFPg=",
    "region_id": "6556f0976a52487eb15f61e2",
    "gender": "male",
    "lastCookie": ".AspNetCore.Cookies=CfDJ8O7INixjpt9Ktfx8f-0_TdQDsyroOMtqPEFZg-Aqmm5vzxBnWuKLJeVbZRN39yfeILuxcH2TwXUbW2WC6rt7H7Q79IclRJiCvNJAq6sSDbjf4Bs5DCiulv56kpsDUFhQnwPc64iiO7nkM62-_eBJNet1CcWx4byyCv5b9x7w9HFJZ2A6noC3P4jvOxmr_cClPi0fpP8BVEWDwrVmLQ3qT55XXgyOGl98m8wiPiyH0yZex3GFr_VgO7ws8gvGiV8AIaGF0-XOM3s8704B7Rc5_fNrb6GwixGdyxvXagQFyu1riFEFWi-XnGdgG3D8SpAdJ0mdbNlHfqC625tVt6qzkg4RCIPUiYNBQr1wmQTS209DCLhWhYf8tmwleNQkFg1Ik6HzoOCHhbqI0KyUbJlHqo8QTm1VijGOvKgz0gqXfatILdIJByIFoYNSSw9QQha4Iw"
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
  



