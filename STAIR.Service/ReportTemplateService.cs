using USBA.Data.Repository;
using USBA.Data.Infrastructure;
using USBA.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USBA.Core.Common;
using USBA.LoggerService;

namespace USBA.Service
{
    public interface IReportTemplateService
    {

        bool CreateReportTemplate(ReportTemplate reportTemplate);
        bool UpdateReportTemplate(ReportTemplate reportTemplate);
        bool DeleteReportTemplate(int id);
        ReportTemplate GetReportTemplate(int id);
        
        IEnumerable<ReportTemplate> GetAllReportTemplate();
        void SaveRecord();


        List<ReportTemplate> GetReportTemplatesByFor(string templatefor);
    }

    public class ReportTemplateService : IReportTemplateService
    {
        private readonly IReportTemplateRepository reportTemplateRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly LoggingService logger = new LoggingService(typeof(ReportTemplateService));

        public ReportTemplateService()
        {
        }
                
        public ReportTemplateService(IReportTemplateRepository reportTemplateRepository, IUnitOfWork unitOfWork)
        {
            this.reportTemplateRepository = reportTemplateRepository;
            this.unitOfWork = unitOfWork;
        }
        public List<ReportTemplate> GetReportTemplatesByFor(string templatefor)
        {
           List<ReportTemplate> templates= reportTemplateRepository.GetMany(a => a.RreportFor == templatefor).ToList();
           return templates;
        }

        public bool CreateReportTemplate(ReportTemplate reportTemplate)
        {
            bool isSuccess = true;
            try
            {
                reportTemplateRepository.Add(reportTemplate);                
                this.SaveRecord();
                ServiceUtil<ReportTemplate>.WriteActionLog(reportTemplate.Id, ENUMOperation.CREATE, reportTemplate);
            }
            catch(Exception ex)
            {
                isSuccess = false;
                logger.Error("Error in creating ReportTemplate", ex);
            }
            return isSuccess;
        }

        public bool UpdateReportTemplate(ReportTemplate reportTemplate)
        {
            bool isSuccess = true;
            try
            {
                reportTemplateRepository.Update(reportTemplate);
                this.SaveRecord();
                ServiceUtil<ReportTemplate>.WriteActionLog(reportTemplate.Id, ENUMOperation.UPDATE, reportTemplate);
            }
            catch(Exception ex)
            {
                isSuccess = false;
                logger.Error("Error in updating ReportTemplate", ex);
            }
            return isSuccess;
        }

        public bool DeleteReportTemplate(int id)
        {
            bool isSuccess = true;
            var reportTemplate = reportTemplateRepository.GetById(id);
            try
            {
                reportTemplateRepository.Delete(reportTemplate);
                SaveRecord();
                ServiceUtil<ReportTemplate>.WriteActionLog(id, ENUMOperation.DELETE);
            }
            catch(Exception ex)
            {
                isSuccess = false;
                logger.Error("Error in deleting ReportTemplate", ex);
            }
            return isSuccess;
        }

        public ReportTemplate GetReportTemplate(int id)
        {
            return reportTemplateRepository.GetById(id);
        }
               
        public IEnumerable<ReportTemplate> GetAllReportTemplate()
        {
            return reportTemplateRepository.GetAll();
        }

        public void SaveRecord()
        {
            unitOfWork.Commit();
        }
    }
}
