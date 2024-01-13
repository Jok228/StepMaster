
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
    "nickname": "ChyDICK",
    "fullname": "RBV",
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
    "group_name": "Шаги за день",
    "name": "10 000",
    "id": 1,
    "aws_path": "StepMaster/Titles/Achievements/1@Шаги за день/1@10 000.svg"
  },
  {
    "groupId": 1,
    "distance": 20000,
    "type": "achievement",
    "timeDay": 0,
    "group_name": "Шаги за день",
    "name": "20 000",
    "id": 2,
    "aws_path": "StepMaster/Titles/Achievements/1@Шаги за день/2@20 000.svg"
  },
  {
    "groupId": 1,
    "distance": 30000,
    "type": "achievement",
    "timeDay": 0,
    "group_name": "Шаги за день",
    "name": "30 000",
    "id": 3,
    "aws_path": "StepMaster/Titles/Achievements/1@Шаги за день/3@30 000.svg"
  },
  {
    "groupId": 1,
    "distance": 40000,
    "type": "achievement",
    "timeDay": 0,
    "group_name": "Шаги за день",
    "name": "40 000",
    "id": 4,
    "aws_path": "StepMaster/Titles/Achievements/1@Шаги за день/4@40 000.svg"
  },
  {
    "groupId": 1,
    "distance": 50000,
    "type": "achievement",
    "timeDay": 0,
    "group_name": "Шаги за день",
    "name": "50 000",
    "id": 5,
    "aws_path": "StepMaster/Titles/Achievements/1@Шаги за день/5@50 000.svg"
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 15,
    "group_name": "Дни подряд",
    "name": "15 дней",
    "id": 2,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/2@15 дней.svg"
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 7,
    "group_name": "Дни подряд",
    "name": "7 дней",
    "id": 1,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/1@7 дней.svg"
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 30,
    "group_name": "Дни подряд",
    "name": "30 дней",
    "id": 3,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/3@30 дней.svg"
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 60,
    "group_name": "Дни подряд",
    "name": "60 дней",
    "id": 4,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/4@60 дней.svg"
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 90,
    "group_name": "Дни подряд",
    "name": "90 дней",
    "id": 5,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/5@90 дней.svg"
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 120,
    "group_name": "Дни подряд",
    "name": "120 дней",
    "id": 6,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/6@120 дней.svg"
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 150,
    "group_name": "Дни подряд",
    "name": "150 дней",
    "id": 7,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/7@150 дней.svg"
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 180,
    "group_name": "Дни подряд",
    "name": "180 дней",
    "id": 8,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/8@180 дней.svg"
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 240,
    "group_name": "Дни подряд",
    "name": "240 дней",
    "id": 9,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/9@240 дней.svg"
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 270,
    "group_name": "Дни подряд",
    "name": "270 дней",
    "id": 10,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/10@270 дней.svg"
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 300,
    "group_name": "Дни подряд",
    "name": "300 дней",
    "id": 11,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/11@300 дней.svg"
  },
  {
    "groupId": 2,
    "distance": 0,
    "type": "achievement",
    "timeDay": 365,
    "group_name": "Дни подряд",
    "name": "365 дней",
    "id": 12,
    "aws_path": "StepMaster/Titles/Achievements/2@Дни подряд/12@365 дней.svg"
  },
  {
    "groupId": 3,
    "distance": 14290,
    "type": "achievement",
    "timeDay": null,
    "group_name": "Общие шаги, км",
    "name": "10 км",
    "id": 1,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/1@10 км.svg"
  },
  {
    "groupId": 3,
    "distance": 42870,
    "type": "achievement",
    "timeDay": null,
    "group_name": "Общие шаги, км",
    "name": "30 км",
    "id": 2,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/2@30 км.svg"
  },
  {
    "groupId": 3,
    "distance": 85740,
    "type": "achievement",
    "timeDay": null,
    "group_name": "Общие шаги, км",
    "name": "60 км",
    "id": 3,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/3@60 км.svg"
  },
  {
    "groupId": 3,
    "distance": 142900,
    "type": "achievement",
    "timeDay": null,
    "group_name": "Общие шаги, км",
    "name": "100 км",
    "id": 4,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/4@100 км.svg"
  },
  {
    "groupId": 3,
    "distance": 285800,
    "type": "achievement",
    "timeDay": null,
    "group_name": "Общие шаги, км",
    "name": "200 км",
    "id": 5,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/5@200 км.svg"
  },
  {
    "groupId": 3,
    "distance": 428700,
    "type": "achievement",
    "timeDay": null,
    "name": "300 км",
    "group_name": "Общие шаги, км",
    "id": 6,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/6@300 км.svg"
  },
  {
    "groupId": 3,
    "distance": 714500,
    "type": "achievement",
    "timeDay": null,
    "name": "500 км",
    "group_name": "Общие шаги, км",
    "id": 7,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/7@500 км.svg"
  },
  {
    "groupId": 3,
    "distance": 1429000,
    "type": "achievement",
    "timeDay": null,
    "group_name": "Общие шаги, км",
    "name": "1000 км",
    "id": 8,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/8@1000 км.svg"
  },
  {
    "groupId": 3,
    "distance": 2143500,
    "type": "achievement",
    "timeDay": null,
    "name": "1500 км",
    "group_name": "Общие шаги, км",
    "id": 9,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/9@1500 км.svg"
  },
  {
    "groupId": 3,
    "distance": 2858000,
    "type": "achievement",
    "group_name": "Общие шаги, км",
    "timeDay": null,
    "name": "2000 км",
    "id": 10,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/10@2000 км.svg"
  },
  {
    "groupId": 3,
    "distance": 3572500,
    "type": "achievement",
    "timeDay": null,
    "name": "2500 км",
    "group_name": "Общие шаги, км",
    "id": 11,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/11@2500 км.svg"
  },
  {
    "groupId": 3,
    "distance": 4287000,
    "type": "achievement",
    "timeDay": null,
    "group_name": "Общие шаги, км",
    "name": "3000 км",
    "id": 12,
    "aws_path": "StepMaster/Titles/Achievements/3@Общие шаги, км/12@3000 км.svg"
  },
  {
    "groupId": 4,
    "distance": 500000,
    "type": "achievement",
    "timeDay": null,
    "group_name": "Общие шаги",
    "name": "500 тыс. шагов",
    "id": 1,
    "aws_path": "StepMaster/Titles/Achievements/4@Общие шаги/1@500 тыс. шагов.svg"
  },
  {
    "groupId": 4,
    "distance": 1000000,
    "type": "achievement",
    "timeDay": null,
    "group_name": "Общие шаги",
    "name": "1 млн. шагов",
    "id": 2,
    "aws_path": "StepMaster/Titles/Achievements/4@Общие шаги/2@1 млн. шагов.svg"
  },
  {
    "groupId": 4,
    "distance": 1500000,
    "type": "achievement",
    "timeDay": null,
    "group_name": "Общие шаги",
    "name": "1,5 млн. шагов",
    "id": 3,
    "aws_path": "StepMaster/Titles/Achievements/4@Общие шаги/3@1,5 млн. шагов.svg"
  },
  {
    "groupId": 4,
    "distance": 2000000,
    "type": "achievement",
    "timeDay": null,
    "group_name": "Общие шаги",
    "name": "2 млн. шагов",
    "id": 4,
    "aws_path": "StepMaster/Titles/Achievements/4@Общие шаги/4@2 млн. шагов.svg"
  },
  {
    "groupId": 4,
    "distance": 3000000,
    "type": "achievement",
    "timeDay": null,
    "group_name": "Общие шаги",
    "name": "3 млн. шагов",
    "id": 5,
    "aws_path": "StepMaster/Titles/Achievements/4@Общие шаги/5@3 млн. шагов.svg"
  },
  {
    "_id": ObjectId('659aad7ca77bc25fee0f5bb2'),
    "groupId": 1,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "group_name": "Младший состав",
    "name": "Стажер",
    "id": 1,
    "aws_path": "StepMaster/Titles/Grades/1@Младший состав/1@Стажер.svg"
  },
  {
    "groupId": 1,
    "distance": 100000,
    "type": "grade",
    "timeDay": 4,
    "group_name": "Младший состав",
    "name": "Участник",
    "id": 2,
    "aws_path": "StepMaster/Titles/Grades/1@Младший состав/2@Участник.svg"
  },
  {
    "groupId": 1,
    "distance": 200000,
    "type": "grade",
    "timeDay": 7,
    "group_name": "Младший состав",
    "name": "Ст участник",
    "id": 3,
    "aws_path": "StepMaster/Titles/Grades/1@Младший состав/3@Ст участник.svg"
  },
  {
    "groupId": 1,
    "distance": 300000,
    "type": "grade",
    "timeDay": 10,
    "group_name": "Младший состав",
    "name": "Ходок",
    "id": 4,
    "aws_path": "StepMaster/Titles/Grades/1@Младший состав/4@Ходок.svg"
  },
  {
    "groupId": 1,
    "distance": 350000,
    "type": "grade",
    "timeDay": 12,
    "group_name": "Младший состав",
    "name": "Мастер",
    "id": 5,
    "aws_path": "StepMaster/Titles/Grades/1@Младший состав/5@Мастер.svg"
  },
  {
    "groupId": 1,
    "distance": 450000,
    "type": "grade",
    "timeDay": 14,
    "group_name": "Младший состав",
    "name": "Атлет",
    "id": 6,
    "aws_path": "StepMaster/Titles/Grades/1@Младший состав/6@Атлет.svg"
  },
  {
    "groupId": 1,
    "distance": 500000,
    "type": "grade",
    "timeDay": 17,
    "group_name": "Младший состав",
    "name": "Ст атлет",
    "id": 7,
    "aws_path": "StepMaster/Titles/Grades/1@Младший состав/7@Ст атлет.svg"
  },
  {
    "groupId": 1,
    "distance": 600000,
    "type": "grade",
    "timeDay": 20,
    "group_name": "Младший состав",
    "name": "Мастер атлет",
    "id": 8,
    "aws_path": "StepMaster/Titles/Grades/1@Младший состав/8@Мастер атлет.svg"
  },
  {
    "groupId": 2,
    "distance": 700000,
    "type": "grade",
    "timeDay": 24,
    "group_name": "Средний состав",
    "name": "Мл пехотинец",
    "id": 1,
    "aws_path": "StepMaster/Titles/Grades/2@Средний состав/1@Мл пехотинец.svg"
  },
  {
    "groupId": 2,
    "distance": 800000,
    "type": "grade",
    "timeDay": 27,
    "name": "Пехотинец",
    "group_name": "Средний состав",
    "id": 2,
    "aws_path": "StepMaster/Titles/Grades/2@Средний состав/2@Пехотинец.svg"
  },
  {
    "groupId": 2,
    "distance": 900000,
    "type": "grade",
    "timeDay": 30,
    "name": "Ст пехотинец",
    "group_name": "Средний состав",
    "id": 3,
    "aws_path": "StepMaster/Titles/Grades/2@Средний состав/3@Ст пехотинец.svg"
  },
  {
    "groupId": 2,
    "distance": 1000000,
    "type": "grade",
    "timeDay": 34,
    "name": "Пехотинец 1 ранга",
    "group_name": "Средний состав",
    "id": 4,
    "aws_path": "StepMaster/Titles/Grades/2@Средний состав/4@Пехотинец 1 ранга.svg"
  },
  {
    "groupId": 2,
    "distance": 1100000,
    "type": "grade",
    "timeDay": 38,
    "name": "Олимпиец",
    "group_name": "Средний состав",
    "id": 5,
    "aws_path": "StepMaster/Titles/Grades/2@Средний состав/5@Олимпиец.svg"
  },
  {
    "groupId": 2,
    "distance": 1200000,
    "type": "grade",
    "timeDay": 40,
    "name": "Спартанец",
    "group_name": "Средний состав",
    "id": 6,
    "aws_path": "StepMaster/Titles/Grades/2@Средний состав/6@Спартанец.svg"
  },
  {
    "groupId": 2,
    "distance": 1300000,
    "type": "grade",
    "group_name": "Средний состав",
    "timeDay": 44,
    "name": "Гоплит",
    "id": 7,
    "aws_path": "StepMaster/Titles/Grades/2@Средний состав/7@Гоплит.svg"
  },
  {
    "groupId": 3,
    "distance": 1500000,
    "type": "grade",
    "timeDay": 50,
    "name": "Чемпион",
    "group_name": "Старший состав",
    "id": 1,
    "aws_path": "StepMaster/Titles/Grades/3@Старший состав/1@Чемпион.svg"
  },
  {
    "groupId": 3,
    "distance": 1700000,
    "type": "grade",
    "timeDay": 57,
    "name": "Чемпион 1 ранга",
    "group_name": "Старший состав",
    "id": 2,
    "aws_path": "StepMaster/Titles/Grades/3@Старший состав/2@Чемпион 1 ранга.svg"
  },
  {
    "groupId": 3,
    "distance": 1900000,
    "type": "grade",
    "timeDay": 64,
    "name": "Мастер чемпион",
    "group_name": "Старший состав",
    "id": 3,
    "aws_path": "StepMaster/Titles/Grades/3@Старший состав/3@Мастер чемпион.svg"
  },
  {
    "groupId": 4,
    "distance": 2200000,
    "type": "grade",
    "timeDay": 74,
    "name": "Герой 3 ранга",
    "group_name": "Высший состав",
    "id": 1,
    "aws_path": "StepMaster/Titles/Grades/4@Высший состав/1@Герой 3 ранга.svg"
  },
  {
    "groupId": 4,
    "distance": 2500000,
    "type": "grade",
    "timeDay": 84,
    "name": "Герой 2 ранга",
    "group_name": "Высший состав",
    "id": 2,
    "aws_path": "StepMaster/Titles/Grades/4@Высший состав/2@Герой 2 ранга.svg"
  },
  {
    "groupId": 4,
    "distance": 2800000,
    "type": "grade",
    "timeDay": 94,
    "name": "Герой 1 ранга",
    "group_name": "Высший состав",
    "id": 3,
    "aws_path": "StepMaster/Titles/Grades/4@Высший состав/3@Герой 1 ранга.svg"
  },
  {
    "groupId": 4,
    "distance": 3100000,
    "type": "grade",
    "timeDay": 104,
    "name": "Маршал шагов",
    "group_name": "Высший состав",
    "id": 4,
    "aws_path": "StepMaster/Titles/Grades/4@Высший состав/4@Маршал шагов.svg"
  },
  {
    "groupId": 5,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "name": "5 степень",
    "group_name": "Лучшие в регионе",
    "id": 1,
    "aws_path": "StepMaster/Titles/Grades/5@Лучшие в регионе/1@5 степень.svg"
  },
  {
    "groupId": 5,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "group_name": "Лучшие в регионе",
    "name": "4 степень",
    "id": 2,
    "aws_path": "StepMaster/Titles/Grades/5@Лучшие в регионе/2@4 степень.svg"
  },
  {
    "groupId": 5,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "group_name": "Лучшие в регионе",
    "name": "3 степень",
    "id": 3,
    "aws_path": "StepMaster/Titles/Grades/5@Лучшие в регионе/3@3 степень.svg"
  },
  {
    "groupId": 5,
    "distance": 0,
    "type": "grade",
    "group_name": "Лучшие в регионе",
    "timeDay": 0,
    "name": "2 степень",
    "id": 4,
    "aws_path": "StepMaster/Titles/Grades/5@Лучшие в регионе/4@2 степень.svg"
  },
  {
    "groupId": 5,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "name": "1 степень",
    "group_name": "Лучшие в регионе",
    "id": 5,
    "aws_path": "StepMaster/Titles/Grades/5@Лучшие в регионе/5@1 степень.svg"
  },
  {
    "groupId": 6,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "name": "5 степень",
    "group_name": "Лучшие в стране (месяц)",
    "id": 1,
    "aws_path": "StepMaster/Titles/Grades/6@Лучшие в стране (месяц)/1@5 степень.svg"
  },
  {
    "groupId": 6,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "group_name": "Лучшие в стране (месяц)",
    "name": "4 степень",
    "id": 2,
    "aws_path": "StepMaster/Titles/Grades/6@Лучшие в стране (месяц)/2@4 степень.svg"
  },
  {
    "groupId": 6,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "name": "3 степень",
    "group_name": "Лучшие в стране (месяц)",
    "id": 3,
    "aws_path": "StepMaster/Titles/Grades/6@Лучшие в стране (месяц)/3@3 степень.svg"
  },
  {
    "groupId": 6,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "group_name": "Лучшие в стране (месяц)",
    "name": "2 степень",
    "id": 4,
    "aws_path": "StepMaster/Titles/Grades/6@Лучшие в стране (месяц)/4@2 степень.svg"
  },
  {
    "groupId": 6,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "name": "1 степень",
    "group_name": "Лучшие в стране (месяц)",
    "id": 5,
    "aws_path": "StepMaster/Titles/Grades/6@Лучшие в стране (месяц)/5@1 степень.svg"
  },
  {
    "groupId": 7,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "name": "3 степень",
    "group_name": "Лучшие в стране (год)",
    "id": 1,
    "aws_path": "StepMaster/Titles/Grades/7@Лучшие в стране (год)/1@3 степень.svg"
  },
  {
    "groupId": 7,
    "distance": 0,
    "type": "grade",
    "timeDay": 0,
    "name": "2 степень",
    "group_name": "Лучшие в стране (год)",
    "id": 2,
    "aws_path": "StepMaster/Titles/Grades/7@Лучшие в стране (год)/2@2 степень.svg"
  },
  {
    "groupId": 7,
    "distance": 0,
    "type": "grade",
    "group_name": "Лучшие в стране (год)",
    "timeDay": 0,
    "name": "1 степень",
    "id": 3,
    "aws_path": "StepMaster/Titles/Grades/7@Лучшие в стране (год)/3@1 степень.svg"
  },
  {
    "groupId": 8,
    "distance": 0,
    "type": "grade",
    "group_name": "Месячная удача",
    "timeDay": 0,
    "name": "Месячная удача",
    "id": 1,
    "aws_path": "StepMaster/Titles/Grades/8@Месячная удача/1@Месячная удача.svg"
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
  



