using AutoMapper;
using MPI_Report.Models;
using MPI_Report.ViewModels;

namespace MPI_Report.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ConfigureCustomerMappings();
            ConfigureInspectionReportMappings();
            ConfigureEquipmentMappings();
            ConfigureConsumableMappings();
            ConfigureTestEvaluationMappings();
        }

        // =========================================================
        // CUSTOMER MAPPINGS
        // =========================================================

        private void ConfigureCustomerMappings()
        {
            // Entity -> List ViewModel
            CreateMap<Customer, CustomerListViewModel>();

            // Entity -> Form ViewModel
            CreateMap<Customer, CustomerFormViewModel>();

            // Form ViewModel -> Entity
            CreateMap<CustomerFormViewModel, Customer>()
                .ForMember(
                    dest => dest.CreatedDate,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.ModifiedDate,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.InspectionReports,
                    opt => opt.Ignore()
                );
        }


        // =========================================================
        // INSPECTION REPORT MAPPINGS
        // =========================================================

        private void ConfigureInspectionReportMappings()
        {
            // Entity -> List Item ViewModel
            CreateMap<InspectionReport, InspectionReportListItemViewModel>()
                .ForMember(
                    dest => dest.CustomerName,
                    opt => opt.MapFrom(src =>
                        src.Customer != null
                            ? src.Customer.Name
                            : string.Empty)
                );

            // Entity -> Details ViewModel
            CreateMap<InspectionReport, InspectionReportDetailsViewModel>()
                .ForMember(
                    dest => dest.CustomerName,
                    opt => opt.MapFrom(src =>
                        src.Customer != null
                            ? src.Customer.Name
                            : string.Empty)
                );

            // Entity -> Form ViewModel
            CreateMap<InspectionReport, InspectionReportFormViewModel>()
                .ForMember(
                    dest => dest.Equipment,
                    opt => opt.MapFrom(src => src.InspectionEquipments)
                )
                .ForMember(
                    dest => dest.Consumables,
                    opt => opt.MapFrom(src => src.InspectionConsumables)
                )
                .ForMember(
                    dest => dest.TestEvaluations,
                    opt => opt.MapFrom(src => src.TestEvaluations)
                );

            // Form ViewModel -> Entity
            CreateMap<InspectionReportFormViewModel, InspectionReport>()
                .ForMember(
                    dest => dest.CreatedDate,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.ModifiedDate,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.Customer,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.InspectionEquipments,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.InspectionConsumables,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.TestEvaluations,
                    opt => opt.Ignore()
                );
        }


        // =========================================================
        // EQUIPMENT MAPPINGS
        // =========================================================

        private void ConfigureEquipmentMappings()
        {
            // Entity -> ViewModel
            CreateMap<InspectionEquipment, InspectionEquipmentViewModel>();

            // ViewModel -> Entity
            CreateMap<InspectionEquipmentViewModel, InspectionEquipment>()
                .ForMember(
                    dest => dest.InspectionReport,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.InspectionReportId,
                    opt => opt.Ignore()
                );
        }


        // =========================================================
        // CONSUMABLE MAPPINGS
        // =========================================================

        private void ConfigureConsumableMappings()
        {
            // Entity -> ViewModel
            CreateMap<InspectionConsumable, InspectionConsumableViewModel>();

            // ViewModel -> Entity
            CreateMap<InspectionConsumableViewModel, InspectionConsumable>()
                .ForMember(
                    dest => dest.InspectionReport,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.InspectionReportId,
                    opt => opt.Ignore()
                );
        }


        // =========================================================
        // TEST EVALUATION MAPPINGS
        // =========================================================

        private void ConfigureTestEvaluationMappings()
        {
            // Entity -> ViewModel
            CreateMap<TestEvaluation, TestEvaluationViewModel>();

            // ViewModel -> Entity
            CreateMap<TestEvaluationViewModel, TestEvaluation>()
                .ForMember(
                    dest => dest.InspectionReport,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.InspectionReportId,
                    opt => opt.Ignore()
                );
        }
    }
}