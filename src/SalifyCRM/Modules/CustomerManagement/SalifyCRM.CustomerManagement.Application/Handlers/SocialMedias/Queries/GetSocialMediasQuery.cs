using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.SocialMedias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.SocialMedias.Queries
{
    public class GetSocialMediasQuery : IRequest<IDataResult<List<SocialMediaResponse>>>
    {
        public class GetSocialMediasQueryHandler : IRequestHandler<GetSocialMediasQuery, IDataResult<List<SocialMediaResponse>>>
        {
            private readonly ISocialMediaRepository _socialMediaRepository;
            private readonly IMediator _mediator;

            public GetSocialMediasQueryHandler(ISocialMediaRepository socialMediaRepository, IMediator mediator)
            {
                this._socialMediaRepository = socialMediaRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<SocialMediaResponse>>> Handle(GetSocialMediasQuery request, CancellationToken cancellationToken)
            {
                var socialMedias = await _socialMediaRepository.GetListAsync(O => O.IsDeleted == false);

                List<SocialMediaResponse> result = SocialMediaExtensions.ToSocialMediaResponseList(socialMedias.ToList());

                return new SuccessDataResult<List<SocialMediaResponse>>(result.ToList());
            }
        }
    }
}
