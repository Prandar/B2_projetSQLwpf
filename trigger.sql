create trigger negociation_to_achat
ON ACHAT
AFTER INSERT 
as  
begin
	UPDATE produit
	SET etat_prod = 'Vendu' Where id_prod = (select id_prod from inserted);
end


ALTER TABLE achat Drop Column date_deb_op, quantity_op, date_fin_op

select * from produit
insert into achat (id_prod, id_u) values (1004, 1)