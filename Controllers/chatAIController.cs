using Microsoft.AspNetCore.Mvc;
using System.ClientModel;
using WebAPI.models;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Files;

namespace WebAPI.Controllers
{
    public class chatAIController
    {
        private readonly ChatClient _chatClient;

        public chatAIController(ChatClient chatClient)
        {
            _chatClient = chatClient;
        }
        public async Task<IResult> chatvoiAIGPT([FromBody] chatAI message)
        {
            string systemPrompt = @"
            Bạn là một trợ lý ảo chỉ được phép trả lời dựa trên 'BỘ QUY TRÌNH' được cung cấp dưới đây.
            Nhiệm vụ của bạn là trả lời các câu hỏi của người dùng và chỉ dựa vào nội dung trong bộ tài liệu này.
            Nếu thông tin không có trong tài liệu, hãy trả lời chính xác là: 'Thông tin này tôi không nắm rõ!'
            Nghiêm cấm tuyệt đối việc sử dụng kiến thức bên ngoài hoặc tự suy diễn.

            
 BỘ QUY TRÌNH 

Phần I: Tuyển nhân sự
1. Lập kế hoạch và Đăng tin tuyển dụng
•	Nhận yêu cầu tuyển dụng: Tiếp nhận thông tin chi tiết về vị trí cần tuyển từ phòng ban.
•	Xây dựng bài đăng tuyển dụng: Soạn thảo nội dung tin tuyển dụng rõ ràng, hấp dẫn và đầy đủ, bao gồm mô tả công việc, yêu cầu ứng viên và quyền lợi. Tin đăng này cần được duyệt trước khi đăng tải.
2. Tiếp nhận và Sàng lọc hồ sơ ứng viên
•	Tiếp nhận hồ sơ: Nhận hồ sơ xin việc, sơ yếu lý lịch đầy đủ, hồ sơ năng lực (nếu có) và các video giới thiệu (nếu ứng viên cung cấp).
•	Sàng lọc và đánh giá ban đầu: Xem xét các hồ sơ để chọn ra những ứng viên tiềm năng phù hợp với yêu cầu của vị trí.
•	Ứng viên phải nộp hồ sơ xin việc, sơ yếu lý lịch đầy đủ.
3. Phỏng vấn và Đánh giá chuyên sâu
•  Lên lịch phỏng vấn: Sắp xếp thời gian và địa điểm phỏng vấn phù hợp với các ứng viên đã được chọn.
•  Tiến hành phỏng vấn và đánh giá: Thực hiện phỏng vấn chuyên sâu để đánh giá kiến thức, kỹ năng và mức độ phù hợp của ứng viên với vị trí và văn hóa công ty.
•  Đưa ra quyết định: Dựa trên kết quả phỏng vấn, đánh giá ứng viên.
•  Thông báo kết quả:
•	Nếu ứng viên đạt, tiến hành gửi thư mời nhận việc hoặc thông báo chi tiết về ngày bắt đầu làm.
•	Nếu ứng viên không đạt, gửi lời cảm ơn và thông báo kết quả phỏng vấn một cách lịch sự.
4. Hỗ trợ hòa nhập 
•	Tiếp nhận và định hướng: Hỗ trợ nhân sự mới trong quá trình hòa nhập, giới thiệu về công ty, văn hóa và các quy định nội bộ.
•	Hỗ trợ ban đầu: Cung cấp các thông tin cần thiết và giải đáp thắc mắc để nhân sự mới có thể bắt đầu công việc một cách thuận lợi.
5. Đánh giá hiệu suất và Chăm sóc định kỳ
•	Đối với nhân viên văn phòng, thực hiện đánh giá sau 15 ngày: Thực hiện đánh giá hiệu suất của nhân sự mới sau 15 ngày làm việc, lặp lại 3 lần để đảm bảo họ thích nghi tốt.
•	Đối với nhân viên thị trường:
o	Gửi tin nhắn quan tâm vào sáng thứ Hai hàng tuần để duy trì kết nối.
o	Thực hiện một cuộc gọi mỗi tháng để nắm bắt tình hình và lắng nghe những khó khăn, đề xuất từ nhân sự.
Phần II: Tuyển chọn đối tác Nhà phân phối và Đại lý
Để trở thành đối tác phân phối hoặc đại lý, cần chuẩn bị đầy đủ các hồ sơ sau:
1. Hồ sơ bắt buộc:
•	Giấy phép kinh doanh hợp lệ.
•	Căn cước công dân (CCCD) của người đại diện.
•	Thông tin địa chỉ.
•	Đối với Nhà phân phối: cần cung cấp thêm thông tin sau:
o	Mã số thuế doanh nghiệp.
2. Quy trình ký kết và vận hành:
•	Giấy cam kết công nợ: Nếu có phát sinh đơn hàng đầu tiên, đối tác cần ký Giấy cam kết công nợ theo mẫu của công ty.
•	Báo cáo Xuất nhập tồn: Hàng tháng, Nhà phân phối phải thực hiện báo cáo tình hình xuất - nhập - tồn kho và gửi về công ty để xác nhận và ký duyệt.
Phần III: Thanh lý hợp đồng
1. Trình phê duyệt với Giám đốc kinh doanh
•	Toàn bộ hồ sơ đề xuất sẽ được trình lên Giám đốc kinh doanh.
•	Việc thanh lý hợp đồng chỉ được thực hiện khi có sự đồng ý của Giám đốc kinh doanh.
2. Thực hiện thanh lý hợp đồng
•	Sau khi nhận được phê duyệt, bộ phận phụ trách sẽ liên hệ và làm việc trực tiếp với nhà phân phối.
•	Hai bên sẽ cùng nhau thỏa thuận các điều khoản thanh lý.
•	Tiến hành xử lý các vấn đề còn tồn đọng như chuyển đơn hàng, hàng khuyến mãi, loyal, xử lý công nợ và các cam kết khác.
3. Hoàn tất và lưu trữ hồ sơ
•	Sau khi hai bên đã thống nhất và hoàn thành mọi thủ tục, một Biên bản thanh lý hợp đồng chính thức sẽ được ký kết.
•	Tất cả các tài liệu liên quan sẽ được lưu trữ để làm cơ sở đối chiếu trong tương lai.
Phần IV: Xử Lý Đơn Hàng
Khi có đơn hàng mới, quy trình xử lý sẽ được thực hiện như sau:
•	Chuyển đơn hàng xuống kho: Ngay lập tức chuyển thông tin đơn hàng xuống bộ phận kho để chuẩn bị hàng.
•	Xác nhận thanh toán: Xác định thời điểm khách hàng đã hoàn tất việc chuyển khoản thanh toán trong vòng 8 giờ.
•	Tìm kiếm phương tiện vận chuyển: Bắt đầu tìm kiếm xe vận chuyển trong khoảng thời gian từ 36 đến 48 giờ.
•	Phí vận chuyển hàng không vượt quá 2%/giá trị đơn hàng.
•	Theo dõi và cập nhật trạng thái đơn hàng: Giám sát toàn bộ quá trình, bao gồm:
o	Thời gian đóng gói hoàn tất tại kho.
o	Thời gian hàng hóa được xếp lên xe.
o	Thời gian dự kiến hàng đến tay Nhà phân phối.
Phần V: Đổi Trả Hàng Hóa
Khi Nhà phân phối có nhu cầu đổi trả hàng, các bước cần tuân thủ là:
•	Thống kê chi tiết hàng hóa: Nhân viên bán hàng phải lập bảng thống kê chi tiết về số lượng, hạn sử dụng (date) và hóa đơn nhập hàng.
•	Xin phê duyệt: Bắt buộc phải có sự đồng ý của Giám đốc kinh doanh trước khi thực hiện việc gửi trả hàng về.
•	Chi phí vận chuyển: Mọi chi phí cước gửi hàng trả về sẽ do Nhà phân phối chi trả.
Phần VI: Đội ngũ Sale, Giám sát bán hàng, ASM
1. Hoạt động hàng ngày
•	Nhân viên Sale: Cần check-in đầy đủ 16 khách hàng mỗi ngày.
•	Giám sát bán hàng (GSBH):
o	Làm việc theo đúng kế hoạch đã được duyệt.
o	Thực hiện check-in hàng ngày trên hệ thống DMS.
o	Đảm bảo 100% khách hàng/tuyến.


2. Quy định về Kế hoạch làm việc
•	Kế hoạch tháng:
o	GSBH và ASM (Giám đốc bán hàng khu vực) có trách nhiệm nộp kế hoạch làm việc tháng về công ty trước ngày 25 hàng tháng.
o	Giám đốc kinh doanh sẽ phê duyệt các kế hoạch này trước ngày 27 cùng tháng.
•	Thay đổi kế hoạch:
o	Mọi sự thay đổi so với kế hoạch đã được duyệt cần được báo cáo cụ thể và chi tiết cho Ban Giám đốc và bộ phận Admin.
3. Quy định về Công tác phí và Vi phạm
•	Công tác phí:
o	ASM chỉ được thanh toán công tác phí khi di chuyển theo đúng kế hoạch làm việc đã được phê duyệt.
•	Vi phạm quy định:
o	Nếu nhân viên không hoạt động theo đúng kế hoạch đã đăng ký, hành động này sẽ được xem là nghỉ việc không có phép.
Phần VII: Lương
•  Nộp bảng lương: Admin phải nộp bảng lương trước ngày 4 hàng tháng. Từ ngày 4 đến ngày 7, Admin cần liên tục nhắc Giám đốc Kinh doanh duyệt bảng lương để đảm bảo đúng tiến độ.
•  Điều kiện chi trả: Lương chỉ được chi trả khi các Nhà phân phối gửi bảng Xuất Nhập Tồn về công ty.
•  Giao chỉ tiêu bán hàng: Chỉ tiêu bán hàng phải được Giám đốc Kinh doanh giao vào ngày 2 hàng tháng. Nếu đến ngày 3 vẫn chưa nhận được chỉ tiêu mới, Admin sẽ sử dụng chỉ tiêu của tháng cũ để tính toán.
Phần VIII: Xuất tiền, tạm ứng
•  Phê duyệt: Mọi yêu cầu xuất tiền hoặc tạm ứng đều phải có chữ ký của Trưởng bộ phận liên quan.
•  Mục đích sử dụng: Khoản tạm ứng phải được sử dụng đúng mục tiêu đã đề ra.
•  Hoàn ứng: Sau khi công việc hoàn tất, người tạm ứng phải hoàn thiện hồ sơ và thủ tục trong vòng 48 giờ để quyết toán.
Phần IX: Chương trình khuyến mãi, loyal
•  Lên kế hoạch: Trước ngày 3 hàng tháng, cần hoàn tất việc lên kế hoạch cho chương trình khuyến mãi.
•  Chuẩn bị: Mọi hoạt động chuẩn bị (ký duyệt, nhập DMS...) phải được hoàn thành trước ngày 3.
•  Khởi động: Chương trình khuyến mãi sẽ chính thức bắt đầu vào sáng ngày 4 hàng tháng.
                                                                                                   
                                                                                                 Giám đốc kinh doanh

                                                                                                
                                                                                                     Nguyễn Xuân Du


            --- KẾT THÚC BỘ QUY TRÌNH ---
lưu ý chỉ đưa ra tối đa 5 gạch đầu dòng cho các ý
        ";
            var chatMessages = new List<ChatMessage>() {
                ChatMessage.CreateSystemMessage(systemPrompt),
                ChatMessage.CreateUserMessage(message.Input)
            };

            ChatCompletion completion = await _chatClient.CompleteChatAsync(messages: chatMessages, new ChatCompletionOptions
            {
                Temperature = 0
            });
            foreach (var msg in chatMessages)
            {
                Console.WriteLine(msg.Content[0].Text);
            }

            return Results.Ok(new { response = completion.Content[0].Text });
        }
    }
}
