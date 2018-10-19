create proc Inscription(
 @nom varchar(100),
 @prenom varchar(100),
 @ad1 varchar(100),
 @ad2 varchar(100),
 @cp int,
 @ville varchar(100),
 @tel int,
 @mdp varchar(100),
 @mail varchar(100),
 @solde int)

 as

 begin 
	insert into utilisateur(nom_u, prenom_u, adresse1_u, adresse2_u, cp_u, ville_u, tel_u, mdp_u, mail_u, solde_u) 
	values (@nom, @prenom, @ad1, @ad2, @cp, @ville, @tel, @mdp, @mail, @solde)
 end;

 select * from utilisateur

 drop proc Inscription