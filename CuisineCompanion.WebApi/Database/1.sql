CREATE TABLE `category` (
                            `category_id` int NOT NULL AUTO_INCREMENT COMMENT '分类id',
                            `c_name` char(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL COMMENT '名称',
                            PRIMARY KEY (`category_id`),
                            UNIQUE KEY `category_pk` (`c_name`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='分类';

CREATE TABLE `category_item` (
                                 `category_item_id` int NOT NULL AUTO_INCREMENT COMMENT '分类项目id',
                                 `category_id` int NOT NULL COMMENT '分类id',
                                 `t_id` int NOT NULL COMMENT '目标id',
                                 `id_category` int NOT NULL COMMENT '目标类型 1：食材 2：食谱 3: 食谱计划',
                                 PRIMARY KEY (`category_item_id`),
                                 UNIQUE KEY `category_item_pk` (`category_id`,`t_id`,`id_category`),
                                 KEY `category_item_category_category_id_fk` (`category_id`)
) ENGINE=InnoDB AUTO_INCREMENT=84 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='分类项目';

CREATE TABLE `cfg` (
                       `cfg_id` int NOT NULL AUTO_INCREMENT COMMENT 'id',
                       `cfg_k` varchar(30) NOT NULL COMMENT '键',
                       `cfg_v` json NOT NULL COMMENT '值',
                       PRIMARY KEY (`cfg_id`),
                       UNIQUE KEY `cfg_pk` (`cfg_k`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='可能会经常更新的配置';

CREATE TABLE `eating_diary` (
                                `ed_id` int NOT NULL AUTO_INCREMENT COMMENT 'id',
                                `user_id` int NOT NULL COMMENT '用户id',
                                `id_category` tinyint NOT NULL COMMENT '目标类型 0：食材  1：食谱',
                                `tid` int DEFAULT NULL COMMENT '目标id',
                                `dosages` json NOT NULL COMMENT '用量列表,（包含id和用量）',
                                `nutrient_content` json NOT NULL COMMENT '营养元素缓存',
                                `update_time` datetime NOT NULL COMMENT '记录的时间',
                                PRIMARY KEY (`ed_id`),
                                KEY `eating_diary_user_user_id_fk` (`user_id`),
                                CONSTRAINT `eating_diary_user_user_id_fk` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=171 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='饮食日记';

CREATE TABLE `favorite` (
                            `favorite_id` int NOT NULL AUTO_INCREMENT COMMENT '收藏id',
                            `user_id` int NOT NULL COMMENT '用户id',
                            `file_uri` char(60) NOT NULL COMMENT '多媒体文件',
                            `f_name` char(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL COMMENT '名称',
                            `refer` char(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL COMMENT '描述',
                            `authority` tinyint NOT NULL DEFAULT '1' COMMENT '权限 1：私有收藏夹 2：公开菜单收藏夹',
                            `id_category` tinyint NOT NULL DEFAULT '2' COMMENT '收藏类型 1：食材 2：食谱 3：菜单',
                            PRIMARY KEY (`favorite_id`),
                            KEY `favorite_user_user_id_fk` (`user_id`),
                            CONSTRAINT `favorite_user_user_id_fk` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=48 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='收藏';

CREATE TABLE `favorite_item` (
                                 `favorite_item_id` int NOT NULL AUTO_INCREMENT COMMENT '收藏项目id',
                                 `favorite_id` int NOT NULL COMMENT '收藏id',
                                 `t_id` int NOT NULL COMMENT '目标id',
                                 PRIMARY KEY (`favorite_item_id`),
                                 UNIQUE KEY `favorite_item_pk` (`favorite_id`,`t_id`)
) ENGINE=InnoDB AUTO_INCREMENT=84 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='收藏项目';

CREATE TABLE `ingredient` (
                              `Ingredient_id` int NOT NULL AUTO_INCREMENT COMMENT '食材id',
                              `user_id` int NOT NULL COMMENT '创建用户id',
                              `i_name` char(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL COMMENT '名称',
                              `file_uri` char(60) DEFAULT NULL COMMENT '多媒体文件',
                              `refer` char(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL COMMENT '描述',
                              `unit` char(10) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL COMMENT '单位',
                              `quantity` json DEFAULT NULL COMMENT '量',
                              `nutritional_composition` json DEFAULT NULL COMMENT '营养成分 默认净含量100g来计算',
                              `allergy` char(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL COMMENT '过敏信息',
                              `content` double NOT NULL DEFAULT '1' COMMENT '净含量',
                              PRIMARY KEY (`Ingredient_id`),
                              KEY `ingredient_user_user_id_fk` (`user_id`),
                              CONSTRAINT `ingredient_user_user_id_fk` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=49 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='食材';

CREATE TABLE `preparation_step` (
                                    `preparation_step_id` int NOT NULL AUTO_INCREMENT COMMENT 'id',
                                    `recipe_id` int NOT NULL COMMENT '食谱id',
                                    `sequence_number` int NOT NULL COMMENT '顺序编号',
                                    `title` char(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL COMMENT '标题',
                                    `file_uri` char(60) DEFAULT NULL COMMENT '图片路径',
                                    `refer` char(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL COMMENT '描述',
                                    `required_time` time DEFAULT NULL COMMENT '所需时间',
                                    `required_ingredient` text COMMENT '所需食材  食材1.食材2.食材3.....|+(+表示加上食材1.食材2.食材3.....).食材a.食材b....|食材制作时间的比例1.....(数量需跟第二个一致)',
                                    `summary` char(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL COMMENT '总结、技巧说明',
                                    PRIMARY KEY (`preparation_step_id`),
                                    KEY `preparation_step_recipe_recipe_id_fk` (`recipe_id`),
                                    CONSTRAINT `preparation_step_recipe_recipe_id_fk` FOREIGN KEY (`recipe_id`) REFERENCES `recipe` (`recipe_id`)
) ENGINE=InnoDB AUTO_INCREMENT=58 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='制作步骤';

CREATE TABLE `recipe` (
                          `recipe_id` int NOT NULL AUTO_INCREMENT COMMENT 'id',
                          `user_id` int NOT NULL COMMENT '创建用户id',
                          `title` char(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL COMMENT '标题',
                          `r_name` char(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL COMMENT '名称',
                          `file_uri` char(60) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL COMMENT '多媒体文件',
                          `summary` char(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL COMMENT '总结',
                          PRIMARY KEY (`recipe_id`),
                          KEY `recipe____fk` (`user_id`),
                          CONSTRAINT `recipe____fk` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='食谱';

CREATE TABLE `recipe_item` (
                               `recipe_item_id` int NOT NULL AUTO_INCREMENT COMMENT '食谱项目id',
                               `recipe_id` int NOT NULL COMMENT '食谱id',
                               `ingredient_id` int NOT NULL COMMENT '食材id',
                               `dosage` decimal(10,0) NOT NULL COMMENT '用量',
                               PRIMARY KEY (`recipe_item_id`),
                               KEY `recipe_item_recipe_recipe_id_fk` (`recipe_id`),
                               KEY `recipe_item_ingredient_Ingredient_id_fk` (`ingredient_id`),
                               CONSTRAINT `recipe_item_ingredient_Ingredient_id_fk` FOREIGN KEY (`ingredient_id`) REFERENCES `ingredient` (`Ingredient_id`),
                               CONSTRAINT `recipe_item_recipe_recipe_id_fk` FOREIGN KEY (`recipe_id`) REFERENCES `recipe` (`recipe_id`)
) ENGINE=InnoDB AUTO_INCREMENT=47 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='食谱项目';

CREATE TABLE `user` (
                        `user_id` int NOT NULL AUTO_INCREMENT COMMENT '用户id',
                        `u_name` char(20) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL COMMENT '名称',
                        `password` char(44) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL COMMENT '密码',
                        `email` char(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL COMMENT '邮箱',
                        `phone` char(11) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL COMMENT '手机',
                        `Salt` char(49) NOT NULL COMMENT '盐',
                        `gender` tinyint(1) NOT NULL DEFAULT '0' COMMENT '性别 1：男生、0：女生',
                        `birth_date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '生日',
                        `is_logout` tinyint(1) NOT NULL DEFAULT '0' COMMENT '是否注销',
                        PRIMARY KEY (`user_id`),
                        UNIQUE KEY `user_pk` (`u_name`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='用户';

CREATE TABLE `user_physical_info` (
                                      `upi_id` int NOT NULL AUTO_INCREMENT COMMENT 'id',
                                      `u_id` int NOT NULL COMMENT '用户id',
                                      `height` double NOT NULL DEFAULT '120' COMMENT '身高',
                                      `weight` double NOT NULL DEFAULT '30' COMMENT '体重(kg)',
                                      `activity_level` char(20) NOT NULL COMMENT '运动习惯',
                                      `protein_requirement` double NOT NULL COMMENT '蛋白质摄入量',
                                      `fat_percentage` double NOT NULL COMMENT '脂肪供能占比',
                                      `update_time` datetime NOT NULL COMMENT '更新时间',
                                      PRIMARY KEY (`upi_id`),
                                      KEY `user_physical_info_user_user_id_fk` (`u_id`),
                                      CONSTRAINT `user_physical_info_user_user_id_fk` FOREIGN KEY (`u_id`) REFERENCES `user` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=48 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='用户身体数据';