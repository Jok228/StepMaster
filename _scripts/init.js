
db = db.getSiblingDB('admin');

db.auth("root","password")

db = db.getSiblingDB('StepMaster');
db.createCollection('Regions');
db.createCollection('Clans');
db.createCollection('Days');
db.createCollection('Rating');
db.createCollection('User');
db.createCollection('Conditions')
db.Clans.insertMany(
[
  { 
  "_id": ObjectId('65b5195553de0d4b0bbbcf20'),   
  "ratingUsers": [
    {
      "step": 30000,
      "email": "paxar14705@notedns.com",
      "nickname": "Retro"
    }
  ],
  "name": "Тестовый 1 можно удалить",
  "description": "Тестовый клан 1!",
  "region_name": "Республика Адыгея",
  "max_users": 1,
  "owner_user_email": "paxar14705@notedns.com",
  "data_create": new Date()
},
{ 
  "_id": ObjectId('65b5194853de0d4b0bbbcf1f'),   
  "ratingUsers": [
    {
      "step": 10000,
      "email": "test@test.ru",
      "nickname": "Набабках"
    },
    {
      "step": 30000,
      "email": "paxar14705@notedns.com",
      "nickname": "Retro"
    }
  ],
  "name": "Тестовый 2 можно удалить",
  "description": "Тестовый клан 2!",
  "region_name": "Республика Адыгея",
  "max_users": 1,
  "owner_user_email": "test@test.ru",
  "data_create": new Date()
},
{ 
  "_id": ObjectId('65b5190853de0d4b0bbbcf1e'),   
  "ratingUsers": [
    {
      "step": 25000,
      "email": "dante_aligiri@rambler.ru",
      "nickname": "Retro2333"
    }
  ],
  "name": "Тестовый 3 можно удалить",
  "description": "Тестовый клан 3!",
  "region_name": "Республика Адыгея",
  "max_users": 1,
  "owner_user_email": "dante_aligiri@rambler.ru",
  "data_create": new Date()
}
])
db.User.insertMany(
  [{ 
    "email": "paxar14705@notedns.com",
    "nickname": "ChyDICK",
    "fullname": "RBV",
    "role": "user",
    "password": "d5KDX1satAqslCkx8k4tFw==;S069ToBh8HO0g93ZypHRT/65BC2AcFlioJKjffQjAL4=",
    "region_id": "6546398a763aafa12565285e",
    "gender": "male",
    "lastCookie": ".AspNetCore.Cookies=CfDJ8OjRetITZDdClN75avPPfv4yi-q7KbJNUrfV_urkI4dIG3Z_j8yrXvJALIyXi1jPWHBo9WwiMB5B8IJG1XNVgXgC_jAhiroEec_QL17fe2qGZLFaJGJiCNR71a70erq51Wcc-rC639xBd2TV_oU2P-zDQZsnuAw7OvxwqVL9RACg7zaNwIoHGj-dW_gvl6f8Zr0Ba8GFdYMDtBPsSiR1y6ksuR7so4mYNXnhQ6wR-qDHv9SYNJiaL9eSrImcsZBhRerRc4XDjqbIpdwY-A4NKeDP5hw3nZ6cC_a552D38e_25sU2o-LfalCeytWXVgGmIUcAVuwzn_b7SU2vMLZkna8TKp-tIanGBJlVo5qt7I9gDArzi4jGBBSggSudD_JLZ1DZVtJEbxH0b3ILKid6k0DXV1G927RP3YAn-u2Nxs12bPvIN-7hFRbF2LOkH7K_PQ",
    "vipStatus": true,
    "titles": [
      "659aad7ca77bc25fee0f5bb2",
      "65b7aacfb8eabe73b5ac13cf",
      "65b7aacfb8eabe73b5ac13d0",
      "65b7aacfb8eabe73b5ac13d1",
      "65b7aacfb8eabe73b5ac13e0"
    ],
    "selectedTitles": [],
    "blockedUsers": [],
    "requrequestInFriends": [],
    "friends": [],
    "lastBeOnline": new Date()
},
{
  "email": "dante_aligiri@rambler.ru",
  "nickname": "Retro2333",
  "fullname": "Bruh",
  "role": "user",
  "password": "J1KVEBXghKa2RGmE11MudQ==;PL3NSe6YTuQV55+EbqrlMLT07LvHiVyW2KEECT2MZAM=",
  "region_id": "65866c8775b374d0753a79e1",
  "gender": "male",
  "lastCookie": ".AspNetCore.Cookies=CfDJ8OjRetITZDdClN75avPPfv7of9B-IjqOjswRE9yCXkru55GiP_PwFvmc_yZTLn8o0zet4zngpUgvPF-uVIX-AloRDCtfhxKFuc5MgajDQpjWvMs98pl1rQu2wOpxKt3E3iYT6znoVSCD3vf1YcpC6ZoPolp3Y0TA93drt5MiATq3TXaMHMMw3F7sMTpUjdw_EsTdfngn2LL9ylWKUkqVT_lihOBgDE5lK0NEr3vUWVL4R6d7ozCEV8QvHWRwlgBXeWzZnlHPyutY18gzG3g7dSVWOI67d9PLNqipFURYmWinh3G_FnzVayAmXxtVcYci-ZBaz7h3jXZ7BWCimb65RpEsmPI0kNLV1WmhjFLrQLzXKgkUQZzNApKrNrBaiooR6_iSOwoQCSViixpLgd0PjRnwMobEXbpIA3azbQObsJM2PpWNHgGh1o_K3qE13tfb9Q",
  "vipStatus": true,
  "titles": [],
  "selectedTitles": [],
  "blockedUsers": [],
  "requrequestInFriends": [],
  "friends": [],
  "lastBeOnline": new Date()
},
{
   "email": "test@test.ru",
  "nickname": "Тейп Набабках",
  "fullname": "Retro228",
  "role": "user",
  "password": "owl65Sq5U6EYYjnsQsR4fA==;Dn56x/oYuIdXLnBnwdA6iIYnUXa0Y2qNYB5PnCW6ols=",
  "region_id": "65866c8775b374d0753a79e1",
  "gender": "male",
  "lastCookie": ".AspNetCore.Cookies=CfDJ8OjRetITZDdClN75avPPfv7nQjOGL1pI0ku6EDriEOMPAtuSIkd8zVbyAafPjmyY986BzrsyvjO22MPdQfFCEo0Qy8j12SVaubuaORvRnPglOx1_VPTBWOG7QjPatJhzVRFtLAWh0mzo9fd16Djwfyj5uG-KmAgoD2GBp9hsv3uVCsmoqru2BQ9X2m9tTg4xrOm591cogyMs8LS_-LpszzlVOa7c7bGcphq5FYx1-3lHRzIRZSj79PvMvKd7isJ1FvJt8JjXK5sxKudv-KdEYq1VTSA3L44I0cBPlgRsL6eoPg_-rJLUBqAD2quAAucUhsjqZjAJSQ02UcFfOi7IllfgXj_ULCDK0Qokn4slgb_-XUi3el0rnfw1HQgglqJBQTMrehutCS10N1lJJeKluIdiho7Uera6K2UgY8niG49F",
  "vipStatus": true,
  "titles": [
    "659aad7ca77bc25fee0f5bb2"
  ],
  "selectedTitles": [],
  "blockedUsers": [],
  "requrequestInFriends": ["paxar14705@notedns.com","dante_aligiri@rambler.ru"],
  "friends": [],
  "lastBeOnline": new Date()
}
]);

db.Conditions.insertMany([
  {   
    "groupId": 1,
    "type": "achievement",
    "distance": 10000,
    "timeDay": 0,
    "group_name": "Шаги за день",
    "name": "10 000",
    "id": 1,
    "aws_path": "StepMaster/Titles/Achievements/1@Шаги за день/1@10 000.svg",
    "description": "Вам нужно за один день пройти - 10000 шагов"
  },
  {
        "groupId": 1,
    "type": "achievement",
    "distance": 20000,
    "timeDay": 0,
    "group_name": "Шаги за день",
    "name": "20 000",
    "id": 2,
    "aws_path": "StepMaster/Titles/Achievements/1@Шаги за день/2@20 000.svg",
    "description": "Вам нужно за один день пройти - 20000 шагов"
  },
  {
    
    "groupId": 1,
    "type": "achievement",
    "distance": 30000,
    "timeDay": 0,
    "group_name": "Шаги за день",
    "name": "30 000",
    "id": 3,
    "aws_path": "StepMaster/Titles/Achievements/1@Шаги за день/3@30 000.svg",
    "description": "Вам нужно за один день пройти - 30000 шагов"
  },
  {
    
    "groupId": 1,
    "type": "achievement",
    "distance": 40000,
    "timeDay": 0,
    "group_name": "Шаги за день",
    "name": "40 000",
    "id": 4,
    "aws_path": "StepMaster/Titles/Achievements/1@Шаги за день/4@40 000.svg",
    "description": "Вам нужно за один день пройти - 40000 шагов"
  },
  {
    
    "groupId": 1,
    "type": "achievement",
    "distance": 50000,
    "timeDay": 0,
    "group_name": "Шаги за день",
    "name": "50 000",
    "id": 5,
    "aws_path": "StepMaster/Titles/Achievements/1@Шаги за день/5@50 000.svg",
    "description": "Вам нужно за один день пройти - 50000 шагов"
  },
  {
   
    "groupId": 2,
    "type": "achievement",
    "distance": null,
    "timeDay": 15,
    "group_name": "Дни подряд",
    "name": "15 дней",
    "id": 2,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/2@15 дней.svg",
    "description": "Для получения этого достижения вам нужно достигать поставленной цели на протяжении - 15 дней"
  },
  {
    
    "groupId": 2,
    "type": "achievement",
    "distance": null,
    "timeDay": 7,
    "group_name": "Дни подряд",
    "name": "7 дней",
    "id": 1,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/1@7 дней.svg",
    "description": "Для получения этого достижения вам нужно достигать поставленной цели на протяжении - 7 дней"
  },
  {
    
    "groupId": 2,
    "type": "achievement",
    "distance": null,
    "timeDay": 30,
    "group_name": "Дни подряд",
    "name": "30 дней",
    "id": 3,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/3@30 дней.svg",
    "description": "Для получения этого достижения вам нужно достигать поставленной цели на протяжении - 30 дней"
  },
  {
   
    "groupId": 2,
    "type": "achievement",
    "distance": null,
    "timeDay": 60,
    "group_name": "Дни подряд",
    "name": "60 дней",
    "id": 4,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/4@60 дней.svg",
    "description": "Для получения этого достижения вам нужно достигать поставленной цели на протяжении - 60 дней"
  },
  {
   
    "groupId": 2,
    "type": "achievement",
    "distance": null,
    "timeDay": 90,
    "group_name": "Дни подряд",
    "name": "90 дней",
    "id": 5,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/5@90 дней.svg",
    "description": "Для получения этого достижения вам нужно достигать поставленной цели на протяжении - 90 дней"
  },
  {
  
    "groupId": 2,
    "type": "achievement",
    "distance": null,
    "timeDay": 120,
    "group_name": "Дни подряд",
    "name": "120 дней",
    "id": 6,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/6@120 дней.svg",
    "description": "Для получения этого достижения вам нужно достигать поставленной цели на протяжении - 120 дней"
  },
  {
    
    "groupId": 2,
    "type": "achievement",
    "distance": null,
    "timeDay": 150,
    "group_name": "Дни подряд",
    "name": "150 дней",
    "id": 7,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/7@150 дней.svg",
    "description": "Для получения этого достижения вам нужно достигать поставленной цели на протяжении - 150 дней"
  },
  {
    "groupId": 2,
    "type": "achievement",
    "distance": null,
    "timeDay": 180,
    "group_name": "Дни подряд",
    "name": "180 дней",
    "id": 8,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/8@180 дней.svg",
    "description": "Для получения этого достижения вам нужно достигать поставленной цели на протяжении - 180 дней"
  },
  {
    
    "groupId": 2,
    "type": "achievement",
    "distance": null,
    "timeDay": 240,
    "group_name": "Дни подряд",
    "name": "240 дней",
    "id": 9,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/9@240 дней.svg",
    "description": "Для получения этого достижения вам нужно достигать поставленной цели на протяжении - 240 дней"
  },
  {
    
    "groupId": 2,
    "type": "achievement",
    "distance": null,
    "timeDay": 270,
    "group_name": "Дни подряд",
    "name": "270 дней",
    "id": 10,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/10@270 дней.svg",
    "description": "Для получения этого достижения вам нужно достигать поставленной цели на протяжении - 270 дней"
  },
  {
    
    "groupId": 2,
    "type": "achievement",
    "distance": null,
    "timeDay": 300,
    "group_name": "Дни подряд",
    "name": "300 дней",
    "id": 11,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/11@300 дней.svg",
    "description": "Для получения этого достижения вам нужно достигать поставленной цели на протяжении - 300 дней"
  },
  {
    
    "groupId": 2,
    "type": "achievement",
    "distance": null,
    "timeDay": 365,
    "group_name": "Дни подряд",
    "name": "365 дней",
    "id": 12,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/12@365 дней.svg",
    "description": "Для получения этого достижения вам нужно достигать поставленной цели на протяжении - 365 дней"
  },
  {
    
    "groupId": 3,
    "type": "achievement",
    "distance": 14290,
    "timeDay": null,
    "group_name": "Общие шаги, км",
    "name": "10 км",
    "id": 1,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/1@10 км.svg",
    "description": "Для получения этого  достижения вам нужно суммарно нужно пройти - 10 км"
  },
  {
    
    "groupId": 3,
    "type": "achievement",
    "distance": 42870,
    "timeDay": null,
    "group_name": "Общие шаги, км",
    "name": "30 км",
    "id": 2,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/2@30 км.svg",
    "description": "Для получения этого  достижения вам нужно суммарно нужно пройти - 30 км"
  },
  {
    
    "groupId": 3,
    "type": "achievement",
    "distance": 85740,
    "timeDay": null,
    "group_name": "Общие шаги, км",
    "name": "60 км",
    "id": 3,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/3@60 км.svg",
    "description": "Для получения этого  достижения вам нужно суммарно нужно пройти - 60 км"
  },
  {
    
    "groupId": 3,
    "type": "achievement",
    "distance": 142900,
    "timeDay": null,
    "group_name": "Общие шаги, км",
    "name": "100 км",
    "id": 4,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/4@100 км.svg",
    "description": "Для получения этого  достижения вам нужно суммарно нужно пройти - 100 км"
  },
  {
    
    "groupId": 3,
    "type": "achievement",
    "distance": 285800,
    "timeDay": null,
    "group_name": "Общие шаги, км",
    "name": "200 км",
    "id": 5,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/5@200 км.svg",
    "description": "Для получения этого  достижения вам нужно суммарно нужно пройти - 200 км"
  },
  {
    
    "groupId": 3,
    "type": "achievement",
    "distance": 428700,
    "timeDay": null,
    "group_name": "Общие шаги, км",
    "name": "300 км",
    "id": 6,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/6@300 км.svg",
    "description": "Для получения этого  достижения вам нужно суммарно нужно пройти - 300 км"
  },
  {
    
    "groupId": 3,
    "type": "achievement",
    "distance": 714500,
    "timeDay": null,
    "group_name": "Общие шаги, км",
    "name": "500 км",
    "id": 7,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/7@500 км.svg",
    "description": "Для получения этого  достижения вам нужно суммарно нужно пройти - 500 км"
  },
  {
   
    "groupId": 3,
    "type": "achievement",
    "distance": 1429000,
    "timeDay": null,
    "group_name": "Общие шаги, км",
    "name": "1000 км",
    "id": 8,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/8@1000 км.svg",
    "description": "Для получения этого  достижения вам нужно суммарно нужно пройти - 1000 км"
  },
  {    
    "groupId": 3,
    "type": "achievement",
    "distance": 2143500,
    "timeDay": null,
    "group_name": "Общие шаги, км",
    "name": "1500 км",
    "id": 9,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/9@1500 км.svg",
    "description": "Для получения этого  достижения вам нужно суммарно нужно пройти - 1500 км"
  },
  {
    
    "groupId": 3,
    "type": "achievement",
    "distance": 2858000,
    "timeDay": null,
    "group_name": "Общие шаги, км",
    "name": "2000 км",
    "id": 10,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/10@2000 км.svg",
    "description": "Для получения этого  достижения вам нужно суммарно нужно пройти - 2000 км"
  },
  {
    
    "groupId": 3,
    "type": "achievement",
    "distance": 3572500,
    "timeDay": null,
    "group_name": "Общие шаги, км",
    "name": "2500 км",
    "id": 11,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/11@2500 км.svg",
    "description": "Для получения этого  достижения вам нужно суммарно нужно пройти - 2500 км"
  },
  {
    
    "groupId": 3,
    "type": "achievement",
    "distance": 4287000,
    "timeDay": null,
    "group_name": "Общие шаги, км",
    "name": "3000 км",
    "id": 12,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/12@3000 км.svg",
    "description": "Для получения этого  достижения вам нужно суммарно нужно пройти - 3000 км"
  },
  {
    
    "groupId": 4,
    "type": "achievement",
    "distance": 500000,
    "timeDay": null,
    "group_name": "Общие шаги",
    "name": "500 тыс. шагов",
    "id": 1,
    "aws_path": "StepMaster/Titles/Achievements/4@Общие шаги/1@500 тыс. шагов.svg",
    "description": "Для получения этого достижения вам нужно суммарно нужно пройти - 500000 шагов"
  },
  {
    
    "groupId": 4,
    "type": "achievement",
    "distance": 1000000,
    "timeDay": null,
    "group_name": "Общие шаги",
    "name": "1 млн. шагов",
    "id": 2,
    "aws_path": "StepMaster/Titles/Achievements/4@Общие шаги/2@1 млн. шагов.svg",
    "description": "Для получения этого достижения вам нужно суммарно нужно пройти - 1000000 шагов"
  },
  {
    
    "groupId": 4,
    "type": "achievement",
    "distance": 1500000,
    "timeDay": null,
    "group_name": "Общие шаги",
    "name": "1,5 млн. шагов",
    "id": 3,
    "aws_path": "StepMaster/Titles/Achievements/4@Общие шаги/3@1,5 млн. шагов.svg",
    "description": "Для получения этого достижения вам нужно суммарно нужно пройти - 1500000 шагов"
  },
  {
    
    "groupId": 4,
    "type": "achievement",
    "distance": 2000000,
    "timeDay": null,
    "group_name": "Общие шаги",
    "name": "2 млн. шагов",
    "id": 4,
    "aws_path": "StepMaster/Titles/Achievements/4@Общие шаги/4@2 млн. шагов.svg",
    "description": "Для получения этого достижения вам нужно суммарно нужно пройти - 2000000 шагов"
  },
  {
    
    "groupId": 4,
    "type": "achievement",
    "distance": 3000000,
    "timeDay": null,
    "group_name": "Общие шаги",
    "name": "3 млн. шагов",
    "id": 5,
    "aws_path": "StepMaster/Titles/Achievements/4@Общие шаги/5@3 млн. шагов.svg",
    "description": "Для получения этого достижения вам нужно суммарно нужно пройти - 3000000 шагов"
  },
  {
    "_id": ObjectId('659aad7ca77bc25fee0f5bb2'),
    "groupId": 1,
    "type": "grade",
    "distance": 0,
    "timeDay": null,
    "group_name": "Младший состав",
    "name": "Стажер",
    "id": 1,
    "aws_path": "StepMaster/Titles/Grades/1@Младший состав/1@Стажер.svg",
    "description": "Для получения этого звания вам нужно преодолеть - 0 шагов"
  },
  {
    
    "groupId": 1,
    "type": "grade",
    "distance": 100000,
    "timeDay": null,
    "group_name": "Младший состав",
    "name": "Участник",
    "id": 2,
    "aws_path": "StepMaster/Titles/Grades/1@Младший состав/2@Участник.svg",
    "description": "Для получения этого звания вам нужно преодолеть - 100000 шагов"
  },
  {
    
    "groupId": 1,
    "type": "grade",
    "distance": 200000,
    "timeDay": null,
    "group_name": "Младший состав",
    "name": "Ст участник",
    "id": 3,
    "aws_path": "StepMaster/Titles/Grades/1@Младший состав/3@Ст участник.svg",
    "description": "Для получения этого звания вам нужно преодолеть - 200000 шагов"
  },
  {
    
    "groupId": 1,
    "type": "grade",
    "distance": 300000,
    "timeDay": null,
    "group_name": "Младший состав",
    "name": "Ходок",
    "id": 4,
    "aws_path": "StepMaster/Titles/Grades/1@Младший состав/4@Ходок.svg",
    "description": "Для получения этого звания вам нужно преодолеть - 300000 шагов"
  },
  {
    
    "groupId": 1,
    "type": "grade",
    "distance": 350000,
    "timeDay": null,
    "group_name": "Младший состав",
    "name": "Мастер",
    "id": 5,
    "aws_path": "StepMaster/Titles/Grades/1@Младший состав/5@Мастер.svg",
    "description": "Для получения этого звания вам нужно преодолеть - 350000 шагов"
  },
  {    
    "groupId": 1,
    "type": "grade",
    "distance": 450000,
    "timeDay": null,
    "group_name": "Младший состав",
    "name": "Атлет",
    "id": 6,
    "aws_path": "StepMaster/Titles/Grades/1@Младший состав/6@Атлет.svg",
    "description": "Для получения этого звания вам нужно преодолеть - 450000 шагов"
  },
  {
    "groupId": 1,
    "type": "grade",
    "distance": 500000,
    "timeDay": null,
    "group_name": "Младший состав",
    "name": "Ст атлет",
    "id": 7,
    "aws_path": "StepMaster/Titles/Grades/1@Младший состав/7@Ст атлет.svg",
    "description": "Для получения этого звания вам нужно преодолеть - 500000 шагов"
  },
  {
    
    "groupId": 1,
    "type": "grade",
    "distance": 600000,
    "timeDay": null,
    "group_name": "Младший состав",
    "name": "Мастер атлет",
    "id": 8,
    "aws_path": "StepMaster/Titles/Grades/1@Младший состав/8@Мастер атлет.svg",
    "description": "Для получения этого звания вам нужно преодолеть - 600000 шагов"
  },
  {
    
    "groupId": 2,
    "type": "grade",
    "distance": 700000,
    "timeDay": null,
    "group_name": "Средний состав",
    "name": "Мл пехотинец",
    "id": 1,
    "aws_path": "StepMaster/Titles/Grades/2@Средний состав/1@Мл пехотинец.svg",
    "description": "Для получения этого звания вам нужно преодолеть - 700000 шагов"
  },
  {
    
    "groupId": 2,
    "type": "grade",
    "distance": 800000,
    "timeDay": null,
    "group_name": "Средний состав",
    "name": "Пехотинец",
    "id": 2,
    "aws_path": "StepMaster/Titles/Grades/2@Средний состав/2@Пехотинец.svg",
    "description": "Для получения этого звания вам нужно преодолеть - 800000 шагов"
  },
  {
    
    "groupId": 2,
    "type": "grade",
    "distance": 900000,
    "timeDay": null,
    "group_name": "Средний состав",
    "name": "Ст пехотинец",
    "id": 3,
    "aws_path": "StepMaster/Titles/Grades/2@Средний состав/3@Ст пехотинец.svg",
    "description": "Для получения этого звания вам нужно преодолеть - 900000 шагов"
  },
  {
    
    "groupId": 2,
    "type": "grade",
    "distance": 1000000,
    "timeDay": null,
    "group_name": "Средний состав",
    "name": "Пехотинец 1 ранга",
    "id": 4,
    "aws_path": "StepMaster/Titles/Grades/2@Средний состав/4@Пехотинец 1 ранга.svg",
    "description": "Для получения этого звания вам нужно преодолеть - 1000000 шагов"
  },
  {
    
    "groupId": 2,
    "type": "grade",
    "distance": 1100000,
    "timeDay": null,
    "group_name": "Средний состав",
    "name": "Олимпиец",
    "id": 5,
    "aws_path": "StepMaster/Titles/Grades/2@Средний состав/5@Олимпиец.svg",
    "description": "Для получения этого звания вам нужно преодолеть - 1100000 шагов"
  },
  {
   
    "groupId": 2,
    "type": "grade",
    "distance": 1200000,
    "timeDay": null,
    "group_name": "Средний состав",
    "name": "Спартанец",
    "id": 6,
    "aws_path": "StepMaster/Titles/Grades/2@Средний состав/6@Спартанец.svg",
    "description": "Для получения этого звания вам нужно преодолеть - 1200000 шагов"
  },
  {
    
    "groupId": 2,
    "type": "grade",
    "distance": 1300000,
    "timeDay": null,
    "group_name": "Средний состав",
    "name": "Гоплит",
    "id": 7,
    "aws_path": "StepMaster/Titles/Grades/2@Средний состав/7@Гоплит.svg",
    "description": "Для получения этого звания вам нужно преодолеть - 1300000 шагов"
  },
  {
    
    "groupId": 3,
    "type": "grade",
    "distance": 1500000,
    "timeDay": null,
    "group_name": "Старший состав",
    "name": "Чемпион",
    "id": 1,
    "aws_path": "StepMaster/Titles/Grades/3@Старший состав/1@Чемпион.svg",
    "description": "Для получения этого звания вам нужно преодолеть - 1500000 шагов"
  },
  {
    
    "groupId": 3,
    "type": "grade",
    "distance": 1700000,
    "timeDay": null,
    "group_name": "Старший состав",
    "name": "Чемпион 1 ранга",
    "id": 2,
    "aws_path": "StepMaster/Titles/Grades/3@Старший состав/2@Чемпион 1 ранга.svg",
    "description": "Для получения этого звания вам нужно преодолеть - 1700000 шагов"
  },
  {
    
    "groupId": 3,
    "type": "grade",
    "distance": 1900000,
    "timeDay": null,
    "group_name": "Старший состав",
    "name": "Мастер чемпион",
    "id": 3,
    "aws_path": "StepMaster/Titles/Grades/3@Старший состав/3@Мастер чемпион.svg",
    "description": "Для получения этого звания вам нужно преодолеть - 1900000 шагов"
  },
  {
    
    "groupId": 4,
    "type": "grade",
    "distance": 2200000,
    "timeDay": null,
    "group_name": "Высший состав",
    "name": "Герой 3 ранга",
    "id": 1,
    "aws_path": "StepMaster/Titles/Grades/4@Высший состав/1@Герой 3 ранга.svg",
    "description": "Для получения этого звания вам нужно преодолеть - 2200000 шагов"
  },
  {
    
    "groupId": 4,
    "type": "grade",
    "distance": 2500000,
    "timeDay": null,
    "group_name": "Высший состав",
    "name": "Герой 2 ранга",
    "id": 2,
    "aws_path": "StepMaster/Titles/Grades/4@Высший состав/2@Герой 2 ранга.svg",
    "description": "Для получения этого звания вам нужно преодолеть - 2500000 шагов"
  },
  {
    
    "groupId": 4,
    "type": "grade",
    "distance": 2800000,
    "timeDay": null,
    "group_name": "Высший состав",
    "name": "Герой 1 ранга",
    "id": 3,
    "aws_path": "StepMaster/Titles/Grades/4@Высший состав/3@Герой 1 ранга.svg",
    "description": "Для получения этого звания вам нужно преодолеть - 2800000 шагов"
  },
  {
    
    "groupId": 4,
    "type": "grade",
    "distance": 3100000,
    "timeDay": null,
    "group_name": "Высший состав",
    "name": "Маршал шагов",
    "id": 4,
    "aws_path": "StepMaster/Titles/Grades/4@Высший состав/4@Маршал шагов.svg",
    "description": "Для получения этого звания вам нужно преодолеть - 3100000 шагов"
  },
  {
    
    "groupId": 5,
    "type": "grade",
    "distance": null,
    "timeDay": null,
    "group_name": "Лучшие в регионе",
    "name": "5 степень",
    "id": 1,
    "aws_path": "StepMaster/Titles/Grades/5@Лучшие в регионе/1@5 степень.svg",
    "description": "Для получения этого звания вам нужно стать лучшим в рейтинге региона за месяц"
  },
  {
    
    "groupId": 5,
    "type": "grade",
    "distance": null,
    "timeDay": null,
    "group_name": "Лучшие в регионе",
    "name": "4 степень",
    "id": 2,
    "aws_path": "StepMaster/Titles/Grades/5@Лучшие в регионе/2@4 степень.svg",
    "description": "Для получения этого звания вам нужно стать лучшим в рейтинге региона за месяц"
  },
  {
   
    "groupId": 5,
    "type": "grade",
    "distance": null,
    "timeDay": null,
    "group_name": "Лучшие в регионе",
    "name": "3 степень",
    "id": 3,
    "aws_path": "StepMaster/Titles/Grades/5@Лучшие в регионе/3@3 степень.svg",
    "description": "Для получения этого звания вам нужно стать лучшим в рейтинге региона за месяц"
  },
  {
    
    "groupId": 5,
    "type": "grade",
    "distance": null,
    "timeDay": null,
    "group_name": "Лучшие в регионе",
    "name": "2 степень",
    "id": 4,
    "aws_path": "StepMaster/Titles/Grades/5@Лучшие в регионе/4@2 степень.svg",
    "description": "Для получения этого звания вам нужно стать лучшим в рейтинге региона за месяц"
  },
  {
    
    "groupId": 5,
    "type": "grade",
    "distance": null,
    "timeDay": null,
    "group_name": "Лучшие в регионе",
    "name": "1 степень",
    "id": 5,
    "aws_path": "StepMaster/Titles/Grades/5@Лучшие в регионе/5@1 степень.svg",
    "description": "Для получения этого звания вам нужно стать лучшим в рейтинге региона за месяц"
  },
  {
    
    "groupId": 6,
    "type": "grade",
    "distance": null,
    "timeDay": null,
    "group_name": "Лучшие в стране (месяц)",
    "name": "5 степень",
    "id": 1,
    "aws_path": "StepMaster/Titles/Grades/6@Лучшие в стране (месяц)/1@5 степень.svg",
    "description": "Для получения этого звания вам нужно стать лучшим в рейтинге страны за месяц"
  },
  {
    
    "groupId": 6,
    "type": "grade",
    "distance": null,
    "timeDay": null,
    "group_name": "Лучшие в стране (месяц)",
    "name": "4 степень",
    "id": 2,
    "aws_path": "StepMaster/Titles/Grades/6@Лучшие в стране (месяц)/2@4 степень.svg",
    "description": "Для получения этого звания вам нужно стать лучшим в рейтинге страны за месяц"
  },
  {
    
    "groupId": 6,
    "type": "grade",
    "distance": null,
    "timeDay": null,
    "group_name": "Лучшие в стране (месяц)",
    "name": "3 степень",
    "id": 3,
    "aws_path": "StepMaster/Titles/Grades/6@Лучшие в стране (месяц)/3@3 степень.svg",
    "description": "Для получения этого звания вам нужно стать лучшим в рейтинге страны за месяц"
  },
  {
    
    "groupId": 6,
    "type": "grade",
    "distance": null,
    "timeDay": null,
    "group_name": "Лучшие в стране (месяц)",
    "name": "2 степень",
    "id": 4,
    "aws_path": "StepMaster/Titles/Grades/6@Лучшие в стране (месяц)/4@2 степень.svg",
    "description": "Для получения этого звания вам нужно стать лучшим в рейтинге страны за месяц"
  },
  {
    
    "groupId": 6,
    "type": "grade",
    "distance": null,
    "timeDay": null,
    "group_name": "Лучшие в стране (месяц)",
    "name": "1 степень",
    "id": 5,
    "aws_path": "StepMaster/Titles/Grades/6@Лучшие в стране (месяц)/5@1 степень.svg",
    "description": "Для получения этого звания вам нужно стать лучшим в рейтинге страны за месяц"
  },
  {
    
    "groupId": 7,
    "type": "grade",
    "distance": null,
    "timeDay": null,
    "group_name": "Лучшие в стране (год)",
    "name": "3 степень",
    "id": 1,
    "aws_path": "StepMaster/Titles/Grades/7@Лучшие в стране (год)/1@3 степень.svg",
    "description": "Для получения этого звания вам нужно стать лучшим в рейтинге страны за год"
  },
  {
    
    "groupId": 7,
    "type": "grade",
    "distance": null,
    "timeDay": null,
    "group_name": "Лучшие в стране (год)",
    "name": "2 степень",
    "id": 2,
    "aws_path": "StepMaster/Titles/Grades/7@Лучшие в стране (год)/2@2 степень.svg",
    "description": "Для получения этого звания вам нужно стать лучшим в рейтинге страны за год"
  },
  {
    
    "groupId": 7,
    "type": "grade",
    "distance": null,
    "timeDay": null,
    "group_name": "Лучшие в стране (год)",
    "name": "1 степень",
    "id": 3,
    "aws_path": "StepMaster/Titles/Grades/7@Лучшие в стране (год)/3@1 степень.svg",
    "description": "Для получения этого звания вам нужно стать лучшим в рейтинге страны за год"
  },
  {   
    "groupId": 8,
    "type": "grade",
    "distance": null,
    "timeDay": null,
    "group_name": "Месячная удача",
    "name": "Месячная удача",
    "id": 1,
    "aws_path": "StepMaster/Titles/Grades/8@Месячная удача/1@Месячная удача.svg",
    "description": "Для получения этого звания вам нужно стать победителем денежного розыгрыша"
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
  



