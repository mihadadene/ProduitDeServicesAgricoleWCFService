using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProduitDeServicesAgricoleWCFService
{
    public class DataProvider
    {
        //private object cnx;

        public List<Produit> GetListProduit()
        {
            List<Produit> result = new List<Produit>();
            Produit produit;
            MySqlConnection cnx = new MySqlConnection();
            cnx.ConnectionString = "Server = 127.0.0.1; Uid = root; pwd = root; Database = mysql; ";
            try
            {
                cnx.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "Produits";
                cmd.CommandType = CommandType.TableDirect;
                cmd.Connection = cnx;

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    produit = new Produit();
                    produit.Id = int.Parse(dr["IdProd"].ToString());
                    produit.Nom = dr["NomProd"].ToString();
                    produit.Qty = int.Parse(dr["QtyProd"].ToString());
                    produit.Prix = double.Parse(dr["PrixProd"].ToString());

                    result.Add(produit);
                }
                cmd.Dispose();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(" erreur " + e);
                return null;
            }
            finally
            {
                cnx.Close();
            }
        }

        public string AcheterProduit(int id, int qtyProd)
        {
            int qtyInit = GetQuantite(id);
            MySqlConnection cnx = new MySqlConnection();
            cnx.ConnectionString = "Server = 127.0.0.1; Uid = root; pwd = root; Database = mysql; ";

           if (qtyInit >= qtyProd)
            {
                int qtyNew = qtyInit - qtyProd;
                try
                {

                    cnx.Open();
                    //cree la requete       
                    MySqlCommand cmd = new MySqlCommand();

                    cmd.Connection = cnx;


                    cmd.CommandText = "update produits set qtyProd = @qtyNewProduit WHERE idProd = @pId";

                    cmd.CommandType = CommandType.Text;

                    MySqlParameter qtyNewProduit = new MySqlParameter();
                    qtyNewProduit.Value = qtyNew;
                    qtyNewProduit.ParameterName = "qtyNewProduit";
                    cmd.Parameters.Add(qtyNewProduit);

                    MySqlParameter pId = new MySqlParameter();
                    pId.Value = id;
                    pId.ParameterName = "pId";
                    cmd.Parameters.Add(pId);

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    return "1";

                }
                catch (Exception e)
                {
                    return "Erreur " + e; // il va aussi t afficher les messages d erreur au cas d exceptions
                }
                finally
                {
                    cnx.Close();
                }
            }
            else
            {
                return "0";
            }


        }

        private static int GetQuantite(int id)
        {
            MySqlConnection cnx = new MySqlConnection();
            cnx.ConnectionString = "Server = 127.0.0.1; Uid = root; pwd = root; Database = mysql; ";
            int qty =-1; 
        
            try
            {

                cnx.Open();

                //crée la requete
                string query = "SELECT qtyProd from produits where idProd =@id";

                //Preapration de l'excution de la requete 
                MySqlCommand cmd = new MySqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("id", id);

                //recuperer le curseur
                MySqlDataReader dr = cmd.ExecuteReader();

                //Parcourir le curseur
                while (dr.Read())
                {
                    qty = (int)dr[0];
                }
                cmd.Dispose();
                return qty; //il returne la quantité
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur " + e);
                return qty; //si il y a un erreur il envoie -1
            }
            finally
            {
                cnx.Close();
            }
        }
    }
    
 }

