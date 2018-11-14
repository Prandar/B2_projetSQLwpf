/*Procédure pour l'inscription d'un utilisateur*/
create proc Inscription
(
	 @nom varchar(100),
	 @prenom varchar(100),
	 @ad1 varchar(100),
	 @ad2 varchar(100),
	 @cp int,
	 @ville varchar(100),
	 @tel int,
	 @mdp varchar(100),
	 @mail varchar(100),
	 @solde int
 )

 as

 begin 
	insert into utilisateur(nom_u, prenom_u, adresse1_u, adresse2_u, cp_u, ville_u, tel_u, mdp_u, mail_u, solde_u) 
	values (@nom, @prenom, @ad1, @ad2, @cp, @ville, @tel, @mdp, @mail, @solde)
 end;

 select * from utilisateur

 drop proc Inscription

 /*Procédure pour l'ajout d'un produit*/

 create proc AddProduit
 (
	@nom varchar(100),
	@prix int,
	@etat varchar(100),
	@photo varchar(250),
	@description varchar(250),
	@id_u int,
	@id_cat int
 )

 as

 begin
	insert into produit(nom_prod, prix_prod, etat_prod, photo_prod, description_prod, id_u, id_cat) 
	values (@nom, @prix, @etat, @photo, @description, @id_u, @id_cat)
end; 

drop proc AddProduit


 INSERT INTO categorie(libelle_cat)
VALUES ('Cuisine - Petit déjeuner');

INSERT INTO produit ( nom_prod, prix_prod, etat_prod, photo_prod, description_prod, id_u, id_cat)
VALUES ('Grille-pain MOULINEX',  20.50, 'En Vente', 'B2_ProjetSQLwpf\Image\produits\moulinex_rp_09112018.jpg',
 'Je vend mon grille pain, tres bon etat! A partir de 20€50.', 1, 1);

 select * from categorie


 SELECT Id_u = id_u FROM utilisateur WHERE mail_u = 'antonin@live.fr' AND mdp_u ='antonin'
 