using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Data.Contexts
{
    public class CategoryContextSQL : ICategoryContext
    {
        private readonly Connection _connection;
        public CategoryContextSQL(Connection connection)
        {
            _connection = connection;
        }

        public List<Category> GetAllCategories()
        {
            List<Category> categoryList = new List<Category>();
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllCategories", _connection.conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                _connection.conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow row in dt.Rows)
                {
                    int categoryId = Convert.ToInt32(row["CategoryID"]);
                    string categoryName = row["Name"].ToString();
                    string categoryDescription = row["Description"].ToString();

                    Category category = new Category(categoryId, categoryName, categoryDescription);
                    categoryList.Add(category);
                }
                return categoryList;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _connection.conn.Close();
            }
        }

        public Category GetCategoryById(int categoryId)
        {
            try
            {
                _connection.conn.Open();
                SqlCommand cmd = new SqlCommand("GetCategory", _connection.conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CategoryId", SqlDbType.Int).Value = categoryId;
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                string name = dt.Rows[0]["Name"].ToString();
                string description = dt.Rows[0]["Description"].ToString();
                Category category = new Category(categoryId, name, description);
                return category;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connection.conn.Close();
            }
        }

        public void AddNewCategory(Category newCategory)
        {
            try
            {
                _connection.conn.Open();
                using (SqlCommand cmd = new SqlCommand("AddNewCategory", _connection.conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", SqlDbType.NVarChar).Value = newCategory.Name;
                    cmd.Parameters.AddWithValue("@Description", SqlDbType.NVarChar).Value = newCategory.Description;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connection.conn.Close();
            }
        }

        public void EditCategory(Category category)
        {
            try
            {
                _connection.conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditCategory", _connection.conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = category.Name;
                    cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = category.Description;
                    cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = category.CategoryId;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Category not edited");
            }
            finally
            {
                _connection.conn.Close();
            }
        }

        public void DeleteCategory(int categoryId)
        {
            try
            {
                _connection.conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteCategory", _connection.conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = categoryId;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connection.conn.Close();
            }
        }
    }
}
