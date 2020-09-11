# 1. Unit Test là gì?
Unit Test - Kiểm thử đơn vị là mức kiểm thử nhỏ nhất trong kiểm thử phần mềm, kiểm tra từng đơn vị code một cách riêng biệt như function, method, class...
Nếu như tester thường là quá trình kiểm thử hộp đen, thì Unit Test là quá trình kiểm thử hộp trắng, được thực hiện bởi dev, trong quá trình code.
# 2.Unit Test để làm gì? 
- Unit Test giúp hạn chế bug, phát hiện các bug đơn giản, đảm bảo đơn vị đó chạy đúng với kết quả mong muốn, cũng như trả về các lỗi, ngoại lệ mong muốn.
- Unit test có hiệu quả rõ ràng khi phải maintain code. Vì các test case chính là phản ảnh cụ thể các spec, giúp ta hình dung được bối cảnh logic, luồng hoạt động và kết quả mong muốn trả về. Do đó khi có CR, các dev sau đó có thể dễ dàng nắm bắt, và thực hiện thay đổi. 
- Khoanh vùng các lỗi xảy ra do thay đổi mã nguồn một cách nhanh chóng
Cần nhấn mạnh rằng, unit test không phải là để tìm bug, bởi vì unit test là kiểm tra các đơn vị code một cách riêng rẽ, xác định tính đúng đắn của chương trình một cách độc lập. Nhưng khi chạy chương trình, đó là một sự kết hợp của tất cả các đơn vị, lúc đó, cách tốt nhất để phát hiện bug, vẫn là kiểm thử hộp đen, chạy trương chình.
# 3.Nguyên tắc tạo Unit Test 
- Tên test case cần rõ ràng, nhất quán,thể hiện rõ test case nên dù tên dài cũng không sao.
- Chắc chắn rằng các test case độc lập với nhau, trong một test case không nên gọi đến test case khác.
- Các test case đảm bảo không phụ thuộc nhau cả về data và thứ tự thực hiện.
- Không nên có nhiều assert trong một test case vì khi một điều kiện không thỏa mãn thì các assert khác sẽ bị bỏ qua.
- Sau một thời gian dài, số lượng test case nhiều, thời gian chạy lớn. Nên chia ra nhóm test case cũ và test case mới, test case cũ sẽ chạy với tần suất ít hơn.
- Phân tích tất cả các tình huống có thể xảy ra với mã, từ trường hợp lý tưởng, luồng chạy đúng, tới các luồng phát sinh ngoại lệ.
- Nhập một số lượng đủ lớn các giá trị đầu vào để phát hiện điểm yếu của mã theo nguyên tắc:
    - Nếu nhập giá trị đầu vào hợp lệ thì kết quả trả về cũng phải hợp lệ.
    - Nếu nhập giá trị đầu vào không hợp lệ thì kết quả trả về phải không hợp lệ.
---
1. [Giới thiệu về unit test](./01-introduction-to-unit-testing.md)
2. [MS Unit test framework](./02-msunitest-framework.md)
3. [Mock object](./03-mock-object.md)
4. [Unit test với Dependency Injection](./04-dependency-injection.md)
---

