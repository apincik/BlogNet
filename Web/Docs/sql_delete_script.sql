SET foreign_key_checks = 0;

DELETE FROM cms_album;
DELETE FROM cms_article;
DELETE FROM cms_article_settings;
DELETE FROM cms_category;
DELETE FROM cms_photo;
DELETE FROM seo_onpage;
DELETE FROM web_menu_item;
DELETE FROM web_page_forward;
DELETE FROM web_template_variable;
DELETE FROM user_project;
DELETE FROM user_project_settings;

SET foreign_key_checks = 1;