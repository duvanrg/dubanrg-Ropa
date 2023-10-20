using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApiContext _context;
        private CargoRepository _Cargos ;
        private ClienteRepository _Clientes ;
        private ColorRepository _Colores ;
        private DepartamentoRepository _Departamentos ;
        private DetalleOrdenRepository _DetallesOrdenes ;
        private DetalleVentaRepository _DetallesVentas ;
        private EmpleadoRepository _Empleados ;
        private EmpresaRepository _Empresas ;
        private EstadoRepository _Estados ;
        private FormaPagoRepository _FormasPagos ;
        private GeneroRepository _Generos ;
        private InsumoRepository _Insumos ;
        private InsumoPrendaRepository _InsumosPrendas ;
        private InsumoProveedorRepository _InsumosProveedores ;
        private InventarioRepository _Inventarios ;
        private InventarioTallaRepository _InventariosTallas ;
        private MunicipioRepository _Municipios ;
        private OrdenRepository _Ordenes ;
        private PaisRepository _Paises ;
        private PrendaRepository _Prendas ;
        private ProveedorRepository _Proveedores ;
        private TallaRepository _Tallas ;
        private TipoEstadoRepository _TiposEstados ;
        private TipoPersonaRepository _TiposPersonas ;
        private TipoProteccionRepository _TiposProtecciones ;
        private VentaRepository _Ventas ;

        public UnitOfWork(ApiContext context)
        {
            _context = context;
        }
        public ICargo Cargos
        {
            get
            {
                if(_Cargos == null) _Cargos = new CargoRepository(_context);
                return _Cargos;
            }
        }
        public ICliente Clientes
        {
            get
            {
                if(_Clientes == null) _Clientes = new ClienteRepository(_context);
                return _Clientes;
            }
        }
        public IColor Colores
        {
            get
            {
                if(_Colores == null) _Colores = new ColorRepository(_context);
                return _Colores;
            }
        }
        public IDepartamento Departamentos
        {
            get
            {
                if(_Departamentos == null) _Departamentos = new DepartamentoRepository(_context);
                return _Departamentos;
            }
        }
        public IDetalleOrden DetallesOrdenes
        {
            get
            {
                if(_DetallesOrdenes == null) _DetallesOrdenes = new DetalleOrdenRepository(_context);
                return _DetallesOrdenes;
            }
        }
        public IDetalleVenta DetallesVentas
        {
            get
            {
                if(_DetallesVentas == null) _DetallesVentas = new DetalleVentaRepository(_context);
                return _DetallesVentas;
            }
        }
        public IEmpleado Empleados
        {
            get
            {
                if(_Empleados == null) _Empleados = new EmpleadoRepository(_context);
                return _Empleados;
            }
        }
        public IEmpresa Empresas
        {
            get
            {
                if(_Empresas == null) _Empresas = new EmpresaRepository(_context);
                return _Empresas;
            }
        }
        public IEstado Estados
        {
            get
            {
                if(_Estados == null) _Estados = new EstadoRepository(_context);
                return _Estados;
            }
        }
        public IFormaPago FormasPagos
        {
            get
            {
                if(_FormasPagos == null) _FormasPagos = new FormaPagoRepository(_context);
                return _FormasPagos;
            }
        }
        public IGenero Generos
        {
            get
            {
                if(_Generos == null) _Generos = new GeneroRepository(_context);
                return _Generos;
            }
        }
        public IInsumoPrenda InsumosPrendas
        {
            get
            {
                if(_InsumosPrendas == null) _InsumosPrendas = new InsumoPrendaRepository(_context);
                return _InsumosPrendas;
            }
        }
        public IInsumoProveedor InsumosProveedores
        {
            get
            {
                if(_InsumosProveedores == null) _InsumosProveedores = new InsumoProveedorRepository(_context);
                return _InsumosProveedores;
            }
        }
        public IInsumo Insumos
        {
            get
            {
                if(_Insumos == null) _Insumos = new InsumoRepository(_context);
                return _Insumos;
            }
        }
        public IInventario Inventarios
        {
            get
            {
                if(_Inventarios == null) _Inventarios = new InventarioRepository(_context);
                return _Inventarios;
            }
        }
        public IInventarioTalla InventariosTallas
        {
            get
            {
                if(_InventariosTallas == null) _InventariosTallas = new InventarioTallaRepository(_context);
                return _InventariosTallas;
            }
        }
        
        public IMunicipio Municipios
        {
            get
            {
                if(_Municipios == null) _Municipios = new MunicipioRepository(_context);
                return _Municipios;
            }
        }
        public IOrden Ordenes
        {
            get
            {
                if(_Ordenes == null) _Ordenes = new OrdenRepository(_context);
                return _Ordenes;
            }
        }
        public IPais Paises
        {
            get
            {
                if(_Paises == null) _Paises = new PaisRepository(_context);
                return _Paises;
            }
        }
        public IPrenda Prendas
        {
            get
            {
                if(_Prendas == null) _Prendas = new PrendaRepository(_context);
                return _Prendas;
            }
        }
        public IProveedor Proveedores
        {
            get
            {
                if(_Proveedores == null) _Proveedores = new ProveedorRepository(_context);
                return _Proveedores;
            }
        }
        public ITalla Tallas
        {
            get
            {
                if(_Tallas == null) _Tallas = new TallaRepository(_context);
                return _Tallas;
            }
        }
        public ITipoEstado TiposEstados
        {
            get
            {
                if(_TiposEstados == null) _TiposEstados = new TipoEstadoRepository(_context);
                return _TiposEstados;
            }
        }
        public ITipoPersona TiposPersonas
        {
            get
            {
                if(_TiposPersonas == null) _TiposPersonas = new TipoPersonaRepository(_context);
                return _TiposPersonas;
            }
        }
        public ITipoProteccion TiposProtecciones
        {
            get
            {
                if(_TiposProtecciones == null) _TiposProtecciones = new TipoProteccionRepository(_context);
                return _TiposProtecciones;
            }
        }
        public IVenta Ventas
        {
            get
            {
                if(_Ventas == null) _Ventas = new VentaRepository(_context);
                return _Ventas;
            }
        }
        

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}