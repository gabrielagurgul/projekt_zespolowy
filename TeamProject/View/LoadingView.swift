//
//  LoadingView.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 17/12/2021.
//

import Foundation
import SwiftUI

struct LoadingView: View {
	@Binding var isLoading: Bool
	var body: some View {
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

