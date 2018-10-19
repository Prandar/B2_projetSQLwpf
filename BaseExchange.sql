create database exchange;
use exchange;

drop table utilisateur;
drop table filtre;
drop table categorie;
drop table produit;
drop table possede;
drop table achat;
drop table messager;
drop table courriel;

create table utilisateur(
id_u int identity (1,1) not null,
nom_u varchar(50) not null,
prenom_u varchar (50) not null,
adresse1_u varchar (100) not null,
adresse2_u varchar (100),
cp_u int not null,
ville_u varchar (50) not null,
tel_u int not null,
mdp_u varchar (50) not null,
mail_u varchar (50) not null,
solde_u int default 0,
admin_u bit default 0,
constraint PK_utilisateur primary key (id_u)
)

create table filtre(
id_fi int identity (1,1) not null,
libelle_fi varchar(50) not null,
constraint PK_Filtre primary key (id_fi)
)

create table categorie(
id_cat int identity (1,1) not null,
libelle_cat varchar(50) not null,
constraint PK_Categorie primary key (id_cat)
)

create table produit(
id_prod int identity (1,1) not null,
nom_prod varchar(100) not null,
prix_prod float not null,
etat_prod varchar(20) not null,
photo_prod varchar(250),
description_prod varchar(250) not null,
id_u int not null,
id_cat int not null,
/*id_st int not null,*/
constraint PK_Produit primary key (id_prod),
constraint FK_Utilisateur_produit foreign key (id_u) references utilisateur(id_u),
constraint FK_Categrorie_produit foreign Key (id_cat) references categorie(id_cat)
/*constraint FK_Statue_produit foreign key (id_st) references statue(id_st)*/
)

create table possede(
id_prod int not null,
id_fi int not null,
constraint FK_Produit_possede foreign key (id_prod) references produit(id_prod),
constraint FK_Filtre_possede foreign key (id_fi) references filtre(id_fi)
)

create table achat(
id_op int identity (1,1) not null,
date_deb_op datetime not null,
date_fin_op datetime,
quantity_op int not null,
id_prod int not null,
id_u int not null,
constraint PK_Operation primary key (id_op),
constraint FK_Utilisateur_operation foreign key (id_u) references utilisateur(id_u)
)

create table messager(
id_m int identity (1,1) not null,
contenue_m varchar(250) not null,
id_u int not null,
constraint PK_Messenger primary key (id_u),
constraint FK_Messager_user foreign key (id_u) references utilisateur(id_u)
)

/*create table statue(
id_st int identity(1,1) not null,
libelle_st varchar(50) not null,
constraint PK_Statue primary key (id_st)
)*/





