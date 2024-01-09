
db = db.getSiblingDB('admin');

db.auth("root","password")

db = db.getSiblingDB('StepMaster');
db.createCollection('Regions');
db.createCollection('Days');
db.createCollection('Rating');
db.createCollection('User');
db.createCollection('Conditions')
db.User.insertMany(
  [{   
    "email": "paxar14705@notedns.com",
    "nickname": "Da?sdfsdf",
    "fullname": "Net?",
    "role": "user",    
    "password": "d5KDX1satAqslCkx8k4tFw==;S069ToBh8HO0g93ZypHRT/65BC2AcFlioJKjffQjAL4=",
    "region_id": "6546398a763aafa12565285e",
    "gender": "male",
    "lastCookie": null,
    "vipStatus": true,
    "titles": [],
    "selectedTitles": []
},
{
  "email": "dante_aligiri@rambler.ru",
  "nickname": "Retro2333",
  "fullname": "Bruh",
  "role": "user",
  "password": "J1KVEBXghKa2RGmE11MudQ==;PL3NSe6YTuQV55+EbqrlMLT07LvHiVyW2KEECT2MZAM=",
  "region_id": "65866c8775b374d0753a79e1",
  "gender": "male",
  "lastCookie":null,
  "vipStatus": true,
  "titles": [],
  "selectedTitles": []
},
{
  "email": "test@test.ru",
  "nickname": "Тейп Набабках",
  "fullname": "Retro228",
  "role": "user",
  "password": "owl65Sq5U6EYYjnsQsR4fA==;Dn56x/oYuIdXLnBnwdA6iIYnUXa0Y2qNYB5PnCW6ols=",
  "region_id": "6546398a763aafa12565285e",
  "gender": "male",
  "lastCookie": ".AspNetCore.Cookies=CfDJ8OjRetITZDdClN75avPPfv5ygZRp7ENTO0PQW87ZKSBy1ocjmofQdtIKvCC5by4ksBuR3ixnQlJ56ktnHsF3FvRXMc2qYjdKX2EMu1KdBlSsR87SrG5h1dXWvn8FZD6PDlRsXqoYIsceQLZFPMUCPvfRjJw-RI-WgF4EwQ6zzI_q0GnJ2oiopjrEF5HzDduTZGPICCqMpEERNHG_E-T_ltx-7DkqaNaORWhXjixhEfIEHsh9oQA4q8Wr2TT_RWaI46SPLIOnD7v6uJsvXJ_6NuSld8DysZakCMiURmmwke59-rMjFMv1icR1SeCIQixKaBnZ_sK7sUC7n-s9WZJgRuVSnPo8DUY5naGTpetR1j8iEMRRLSZXc4P4HM-EcdqGRiOI-oLZkhbAxFQg8BCUnvk_xbetipbbKZzgpOAKbPBt",
  "vipStatus": true,
  "titles": [],
  "selectedTitles": []
}
]);

db.Conditions.insertMany([
  {
    "groupId": 1,
    "distance": 10000,
    "type": "achievement",
    "timeDay": 0,
    "name": "10 000",
    "id": 1
  },
  {
    "groupId": 1,
    "distance": 20000,
    "type": "achievement",
    "timeDay": 0,
    "name": "20 000",
    "id": 2
  },
  {
    "groupId": 1,
    "distance": 30000,
    "type": "achievement",
    "timeDay": 0,
    "name": "30 000",
    "id": 3
  },
  {
    "groupId": 1,
    "distance": 40000,
    "type": "achievement",
    "timeDay": 0,
    "name": "40 000",
    "id": 4
  },
  {
    "groupId": 1,
    "distance": 50000,
    "type": "achievement",
    "timeDay": 0,
    "name": "50 000",
    "id": 5
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 15,
    "name": "15 дней",
    "id": 2
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 7,
    "name": "7 дней",
    "id": 1
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 30,
    "name": "30 дней",
    "id": 3
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 60,
    "name": "60 дней",
    "id": 4
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 90,
    "name": "90 дней",
    "id": 5
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 120,
    "name": "120 дней",
    "id": 6
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 150,
    "name": "150 дней",
    "id": 7
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 180,
    "name": "180 дней",
    "id": 8
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 240,
    "name": "240 дней",
    "id": 9
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 270,
    "name": "270 дней",
    "id": 10
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 300,
    "name": "300 дней",
    "id": 11
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 365,
    "name": "365 дней",
    "id": 12
  },
  {
    "groupId": 3,
    "distance": 14290,
    "type": "achievement",
    "timeDay": null,
    "name": "10 км",
    "id": 1
  },
  {
    "groupId": 3,
    "distance": 42870,
    "type": "achievement",
    "timeDay": null,
    "name": "30 км",
    "id": 2
  },
  {
    "groupId": 3,
    "distance": 85740,
    "type": "achievement",
    "timeDay": null,
    "name": "60 км",
    "id": 3
  },
  {
    "groupId": 3,
    "distance": 142900,
    "type": "achievement",
    "timeDay": null,
    "name": "100 км",
    "id": 4
  },
  {
    "groupId": 3,
    "distance": 285800,
    "type": "achievement",
    "timeDay": null,
    "name": "200 км",
    "id": 5
  },
  {
    "groupId": 3,
    "distance": 428700,
    "type": "achievement",
    "timeDay": null,
    "name": "300 км",
    "id": 6
  },
  {
    "groupId": 3,
    "distance": 714500,
    "type": "achievement",
    "timeDay": null,
    "name": "500 км",
    "id": 7
  },
  {
    "groupId": 3,
    "distance": 1429000,
    "type": "achievement",
    "timeDay": null,
    "name": "1000 км",
    "id": 8
  },
  {
    "groupId": 3,
    "distance": 2143500,
    "type": "achievement",
    "timeDay": null,
    "name": "1500 км",
    "id": 9
  },
  {
    "groupId": 3,
    "distance": 2858000,
    "type": "achievement",
    "timeDay": null,
    "name": "2000 км",
    "id": 10
  },
  {
    "groupId": 3,
    "distance": 3572500,
    "type": "achievement",
    "timeDay": null,
    "name": "2500 км",
    "id": 11
  },
  {
    "groupId": 3,
    "distance": 4287000,
    "type": "achievement",
    "timeDay": null,
    "name": "3000 км",
    "id": 12
  },
  {
    "groupId": 4,
    "distance": 500000,
    "type": "achievement",
    "timeDay": null,
    "name": "500 тыс. шагов",
    "id": 1
  },
  {
    "groupId": 4,
    "distance": 1000000,
    "type": "achievement",
    "timeDay": null,
    "name": "1 млн. шагов",
    "id": 2
  },
  {
    "groupId": 4,
    "distance": 1500000,
    "type": "achievement",
    "timeDay": null,
    "name": "1,5 млн. шагов",
    "id": 3
  },
  {
    "groupId": 4,
    "distance": 2000000,
    "type": "achievement",
    "timeDay": null,
    "name": "2 млн. шагов",
    "id": 4
  },
  {    
    "groupId": 4,
    "distance": 3000000,
    "type": "achievement",
    "timeDay": null,
    "name": "3 млн. шагов",
    "id": 5
  },
  {
    
    "groupId": 1,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "name": "Стажер",
    "id": 1
  },
  {
    "groupId": 1,
    "distance": 100000,
    "type": "grade",
    "timeDay": 4,
    "name": "Участник",
    "id": 2
  },
  {
    "groupId": 1,
    "distance": 200000,
    "type": "grade",
    "timeDay": 7,
    "name": "Ст участник",
    "id": 3
  },
  {
    "groupId": 1,
    "distance": 300000,
    "type": "grade",
    "timeDay": 10,
    "name": "Ходок",
    "id": 4
  },
  {
    "groupId": 1,
    "distance": 350000,
    "type": "grade",
    "timeDay": 12,
    "name": "Мастер",
    "id": 5
  },
  {
    "groupId": 1,
    "distance": 450000,
    "type": "grade",
    "timeDay": 14,
    "name": "Атлет",
    "id": 6
  },
  {
    "groupId": 1,
    "distance": 500000,
    "type": "grade",
    "timeDay": 17,
    "name": "Ст атлет",
    "id": 7
  },
  {
    "groupId": 1,
    "distance": 600000,
    "type": "grade",
    "timeDay": 20,
    "name": "Мастер атлет",
    "id": 8
  },
  {
    "groupId": 2,
    "distance": 700000,
    "type": "grade",
    "timeDay": 24,
    "name": "Мл пехотинец",
    "id": 1
  },
  {
    "groupId": 2,
    "distance": 800000,
    "type": "grade",
    "timeDay": 27,
    "name": "Пехотинец",
    "id": 2
  },
  {
    "groupId": 2,
    "distance": 900000,
    "type": "grade",
    "timeDay": 30,
    "name": "Ст пехотинец",
    "id": 3
  },
  {
    "groupId": 2,
    "distance": 1000000,
    "type": "grade",
    "timeDay": 34,
    "name": "Пехотинец 1 ранга",
    "id": 4
  },
  {
    "groupId": 2,
    "distance": 1100000,
    "type": "grade",
    "timeDay": 38,
    "name": "Олимпиец",
    "id": 5
  },
  {
    "groupId": 2,
    "distance": 1200000,
    "type": "grade",
    "timeDay": 40,
    "name": "Спартанец",
    "id": 6
  },
  {
    "groupId": 2,
    "distance": 1300000,
    "type": "grade",
    "timeDay": 44,
    "name": "Гоплит",
    "id": 7
  },
  {
    "groupId": 3,
    "distance": 1500000,
    "type": "grade",
    "timeDay": 50,
    "name": "Чемпион",
    "id": 1
  },
  {
    "groupId": 3,
    "distance": 1700000,
    "type": "grade",
    "timeDay": 57,
    "name": "Чемпион 1 ранга",
    "id": 2
  },
  {
    "groupId": 3,
    "distance": 1900000,
    "type": "grade",
    "timeDay": 64,
    "name": "Мастер чемпион",
    "id": 3
  },
  {
    "groupId": 4,
    "distance": 2200000,
    "type": "grade",
    "timeDay": 74,
    "name": "Герой 3 ранга",
    "id": 1
  },
  {
    "groupId": 4,
    "distance": 2500000,
    "type": "grade",
    "timeDay": 84,
    "name": "Герой 2 ранга",
    "id": 2
  },
  {
    "groupId": 4,
    "distance": 2800000,
    "type": "grade",
    "timeDay": 94,
    "name": "Герой 1 ранга",
    "id": 3
  },
  {
    "groupId": 4,
    "distance": 3100000,
    "type": "grade",
    "timeDay": 104,
    "name": "Мастер шагов",
    "id": 4
  },
  {
    "groupId": 5,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "name": "5 степень",
    "id": 1
  },
  {
    "groupId": 5,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "name": "4 степень",
    "id": 2
  },
  {
    "groupId": 5,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "name": "3 степень",
    "id": 3
  },
  {
    "groupId": 5,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "name": "2 степень",
    "id": 4
  },
  {
    "groupId": 5,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "name": "1 степень",
    "id": 5
  },
  {
    "groupId": 6,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "name": "5 степень",
    "id": 1
  },
  {
    "groupId": 6,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "name": "4 степень",
    "id": 2
  },
  {
    "groupId": 6,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "name": "3 степень",
    "id": 3
  },
  {
    "groupId": 6,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "name": "2 степень",
    "id": 4
  },
  {
    "groupId": 6,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "name": "1 степень",
    "id": 5
  },
  {
    "groupId": 7,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "name": "3 степень",
    "id": 1
  },
  {
    "groupId": 7,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "name": "2 степень",
    "id": 2
  },
  {
    "groupId": 7,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "name": "1 степень",
    "id": 3
  }
])  
  db.Regions.insertMany([ 
    {
      "_id": ObjectId('65866c8775b374d0753a79e1'),
      "fullName": "Республика Адыгея"
    },
    { 
      "_id": ObjectId('6546398a763aafa12565285e'),
      "fullName": "Республика Карелия"
     },
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
  



