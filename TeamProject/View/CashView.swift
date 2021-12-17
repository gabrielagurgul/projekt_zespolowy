//
//  CashView.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 11/12/2021.
//

import SwiftUI

struct CashView: View {
	@Environment(\.colorScheme) var colorScheme
	private let id: Int
	private let image: Image
	private let name: String
	@State var ammount: String = ""
	@State var isLoading = false
	
	var body: some View {
		VStack(alignment: .leading) {
			HStack {
				Text(name)
					.font(.largeTitle)
					.bold()
				Spacer()
			}
			Text(ammount)
				.font(.title)
				.padding([.leading, .top])
			Spacer()
		}
		.padding()
		.frame(maxWidth: .infinity)
		.frame(height: 160)
		.background(alignment: .bottomTrailing) {imageColor}
		.background(.regularMaterial)
		.mask(RoundedRectangle(cornerRadius: 8))
		.shadow(radius: 16)
		.task {
			do {
				isLoading = true
				let response = try await getBudgetBy(id)
				let sum = response.map {$0.amount}.reduce(0,+)
				ammount = "\(sum)"
			} catch let error  {
				print(error.localizedDescription)
				ammount = "Nie udało się przechwycić danych"
			}
		}
	}
	
	@ViewBuilder
	private var imageColor: some View {
		if colorScheme == .dark {
			image
				.resizable()
				.scaledToFit()
				.colorInvert()
				.frame(height: 100)
				.padding([.trailing, .bottom])
		} else {
			image
				.resizable()
				.scaledToFit()
				.frame(height: 100)
				.padding([.trailing, .bottom])
		}

	}
	
	@ViewBuilder
	private var loadingOverlay: some View {
		if isLoading {
			ProgressView()
				.progressViewStyle(CircularProgressViewStyle(tint: .blue))
				.padding(50)
				.background(.regularMaterial)
				.mask(RoundedRectangle(cornerRadius: 8))
				.overlay(alignment: .bottom) {
					Text("Please wait")
				}
		}
	}
}

extension CashView {
	init(budgetType: BudgetType) {
		id = budgetType.id
		image = Image(budgetType.type.lowercased())
		name = budgetType.type
	}
}

struct CashView_Previews: PreviewProvider {
	static var previews: some View {
		CashView(budgetType: BudgetType.budgetTypeMock)
	}
}
