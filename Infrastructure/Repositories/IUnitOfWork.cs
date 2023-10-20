using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Infrastructure.Repositories
{
    public interface IUnitOfWork
    {
        ICargo Cargos { get;}
        ICliente Clientes { get;}
        IColor Colores { get;}
        IDepartamento Departamentos { get;}
        IDetalleOrden DetallesOrdenes { get;}
        IDetalleVenta DetallesVentas { get;}
        IEmpleado Empleados { get;}
        IEmpresa Empresas { get;}
        IEstado Estados { get;}
        IFormaPago FormasPagos { get;}
        IGenero Generos { get;}
        IInsumo Insumos { get;}
        IInsumoPrenda InsumosPrendas { get;}
        IInsumoProveedor InsumosProveedores { get;}
        IInventario Inventarios { get;}
        IInventarioTalla InventariosTallas { get;}
        IMunicipio Municipios { get;}
        IOrden Ordenes { get;}
        IPais Paises { get;}
        IPrenda Prendas { get;}
        IProveedor Proveedores { get;}
        ITalla Tallas { get;}
        ITipoEstado TiposEstados { get;}
        ITipoPersona TiposPersonas { get;}
        ITipoProteccion TiposProtecciones { get;}
        IVenta Ventas { get;}
        Task<int> SaveAsync();
    }
}